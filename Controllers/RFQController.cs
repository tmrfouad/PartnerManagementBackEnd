using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PartnerManagement.Models;
using PartnerManagement.Models.Enumerations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using AutoMapper;
using PartnerManagement.Models.DTOs;

[Authorize]
[Route("[controller]/[action]")]
[EnableCors("AllowAnyOrigin")]
public class RFQController : Controller
{
    CustomersGateContext _context;
    IMapper _mapper;

    public RFQController(CustomersGateContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [AllowAnonymous]
    public string test()
    {
        return "API is working...";
    }

    #region RFQs
    // GET RFQ/Get
    [HttpGet]
    public async Task<IEnumerable<RFQDTO>> Get()
    {
        var items = _context.RFQs
            .Include(r => r.TargetedProduct)
            .Include(r => r.SelectedEdition)
            .ToList();
        var itemsDto = _mapper.Map<IEnumerable<RFQDTO>>(items);
        return await Task.Run(() => itemsDto);
    }

    // GET RFQ/Get/5
    [HttpGet("{id}", Name = "GetRFQ")]
    public async Task<ActionResult> Get(int id)
    {
        var item = _context.RFQs
            .Include(r => r.TargetedProduct)
            .Include(r => r.SelectedEdition)
            .SingleOrDefault(o => o.RFQId == id);

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        var itemDto = _mapper.Map<RFQDTO>(item);
        return await Task.Run(() => new ObjectResult(itemDto));
    }

    // POST RFQ/Post
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> Post([FromBody]RFQDTO rfqDto)
    {
        if (rfqDto == null)
        {
            return await Task.Run(() => BadRequest());
        }

        bool saved = false;

        try
        {
            rfqDto.RFQCode = DateTime.Now.Ticks.ToString();
            rfqDto.SubmissionTime = DateTime.Now;

            var rfq = _mapper.Map<RFQ>(rfqDto);
            _context.RFQs.Add(rfq);
            _context.SaveChanges();
            rfqDto = _mapper.Map(rfq, rfqDto);
            saved = true;
        }
        catch (System.Exception)
        {
            saved = false;
            throw;
        }

        if (!saved)
            return await Task.Run(() => new NoContentResult());

        if (rfqDto.SendEmail)
        {
            bool sent = false;
            try
            {
                var htmlTemplate = "";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MailTemplete.html");
                using (var file = new StreamReader(path))
                {
                    htmlTemplate = file.ReadToEnd();
                }

                string[] tags = new[] { "Name", "phone" };
                string[] tagValues = new[] { rfqDto.ContactPersonEnglishName, rfqDto.ContactPersonMobile };
                MailHelper.sendMail(new MailData
                {
                    Message = new MailMessageData(new[] { rfqDto.ContactPersonEmail })
                    {
                        Body = MailHelper.MessageBody(htmlTemplate, tags, tagValues)
                    },
                    SMTP = new SmtpData()
                });
                sent = true;
            }
            catch (Exception ex)
            {
                sent = false;
                throw ex;
            }

            if (sent)
            {
                RFQAction rfqAction = new RFQAction
                {
                    ActionCode = DateTime.Now.Ticks.ToString(),
                    ActionTime = DateTime.Now,
                    ActionType = ActionType.EmailMessage,
                    Comments = "Automated Email",
                    RepresentativeId = 0,
                    SubmissionTime = DateTime.Now,
                    UniversalIP = rfqDto.UniversalIP
                };
                var newRfq = _context.RFQs.Where(r => r.RFQId == rfqDto.RFQId).Include(r => r.RFQActions).SingleOrDefault();
                newRfq.RFQActions.Add(rfqAction);
                _context.SaveChanges();
            }
        }

        var _rfq = _context.RFQs
            .Where(r => r.RFQId == rfqDto.RFQId)
            .Include(r => r.SelectedEdition)
            .Include(r => r.TargetedProduct)
            .SingleOrDefault();
        rfqDto = _mapper.Map(_rfq, rfqDto);

        return await Task.Run(() => new ObjectResult(rfqDto));
    }

    // POST rfq/sendMail/1/1
    [HttpPost("{id}/{templateId}")]
    public async Task<ActionResult> SendMail(int id, int templateId)
    {
        var mailTemp = _context.EmailTemplates.SingleOrDefault(e => e.Id == templateId);

        if (mailTemp == null)
        {
            await Task.Run(() => NotFound());
        }

        var rfq = _context.RFQs.SingleOrDefault(e => e.RFQId == id);

        if (rfq == null)
        {
            await Task.Run(() => NotFound());
        }

        string[] tags = new[]
        {
            "Address",
            "CompanyEnglishName",
            "ContactPersonEmail",
            "ContactPersonMobile",
            "ContactPersonEnglishName",
            "ContactPersonPosition",
            "Location",
            "PhoneNumber",
            "RFQCode",
            "SelectedEdition",
            "Status",
            "TargetedProduct",
            "Website"
        };
        string[] tagValues = new[]
        {
            rfq.Address,
            rfq.CompanyEnglishName,
            rfq.ContactPersonEmail,
            rfq.ContactPersonEnglishName,
            rfq.ContactPersonMobile,
            rfq.ContactPersonPosition,
            rfq.Location,
            rfq.PhoneNumber,
            rfq.RFQCode,
            rfq.SelectedEdition.EnglishName,
            rfq.Status.ToString(),
            rfq.TargetedProduct.EnglishName,
            rfq.Website
        };
        var mail = new MailData
        {
            Message = new MailMessageData(new[] { rfq.ContactPersonEmail })
            {
                Subject = mailTemp.Subject,
                Body = MailHelper.MessageBody(mailTemp.HtmlTemplate, tags, tagValues)
            },
            SMTP = new SmtpData()
        };

        MailHelper.sendMail(mail);

        return await Task.Run(() => new ObjectResult(mail));
    }

    // PUT RFQ/Put/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody]RFQDTO rfqDto)
    {
        if (rfqDto == null || rfqDto.RFQId != id)
        {
            return await Task.Run(() => BadRequest());
        }

        var orgItem = _context.RFQs.SingleOrDefault(o => o.RFQId == id);
        if (orgItem == null)
        {
            return await Task.Run(() => NotFound());
        }

        orgItem = _mapper.Map(rfqDto, orgItem);
        // orgItem.Address = rfqDto.Address;
        // orgItem.CompanyArabicName = rfqDto.CompanyArabicName;
        // orgItem.CompanyEnglishName = rfqDto.CompanyEnglishName;
        // orgItem.ContactPersonArabicName = rfqDto.ContactPersonArabicName;
        // orgItem.ContactPersonEmail = rfqDto.ContactPersonEmail;
        // orgItem.ContactPersonEnglishName = rfqDto.ContactPersonEnglishName;
        // orgItem.ContactPersonMobile = rfqDto.ContactPersonMobile;
        // orgItem.ContactPersonPosition = rfqDto.ContactPersonPosition;
        // orgItem.Location = rfqDto.Location;
        // orgItem.PhoneNumber = rfqDto.PhoneNumber;
        // orgItem.SelectedEditionId = rfqDto.SelectedEditionId;
        // orgItem.Status = rfqDto.Status;
        // orgItem.SubmissionTime = DateTime.Now;
        // orgItem.TargetedProductId = rfqDto.TargetedProductId;
        // orgItem.UniversalIP = rfqDto.UniversalIP;
        // orgItem.Website = rfqDto.Website;

        _context.RFQs.Update(orgItem);
        _context.SaveChanges();

        var _rfq = _context.RFQs
            .Where(r => r.RFQId == rfqDto.RFQId)
            .Include(r => r.SelectedEdition)
            .Include(r => r.TargetedProduct)
            .SingleOrDefault();
        rfqDto = _mapper.Map(_rfq, rfqDto);

        return await Task.Run(() => new ObjectResult(rfqDto));
    }

    // DELETE RFQ/Delete/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var item = _context.RFQs.SingleOrDefault(o => o.RFQId == id);
        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        _context.RFQs.Remove(item);
        _context.SaveChanges();

        var itemDto = _mapper.Map<RFQDTO>(item);
        return await Task.Run(() => new ObjectResult(itemDto));
    }
    #endregion

    #region Actions
    // GET RFQ/Status/5
    [HttpGet("{id}", Name = "GetRFQStatus")]
    public async Task<ActionResult> Status(int id)
    {
        var item = _context.RFQs
            .Where(o => o.RFQId == id)
            .Include(r => r.RFQActions)
            .ThenInclude(a => a.Representative)
            .Include(r => r.RFQActions)
            .ThenInclude(a => a.RFQActionAtts)
            .FirstOrDefault();

        if (item == null)
        {
            return NotFound();
        }

        var rfqAction = item.RFQActions.SingleOrDefault(a => a.ActionTime == item.RFQActions.Max(a1 => a1.ActionTime));
        var rfqActionDto = _mapper.Map<RFQActionDTO>(rfqAction);
        // var rfqAction = item.RFQActions
        //     .Select(a => new RFQAction
        //     {
        //         ActionCode = a.ActionCode,
        //         ActionTime = a.ActionTime,
        //         ActionType = a.ActionType,
        //         Comments = a.Comments,
        //         RepresentativeId = a.RepresentativeId,
        //         Representative = new Representative
        //         {
        //             Address = a.Representative.Address,
        //             Continuous = a.Representative.Continuous,
        //             Created = a.Representative.Created,
        //             DateOfBirth = a.Representative.DateOfBirth,
        //             Id = a.Representative.Id,
        //             Name = a.Representative.Name,
        //             PersonalPhone = a.Representative.PersonalPhone,
        //             Phone = a.Representative.Phone,
        //             Position = a.Representative.Position,
        //             UniversalIP = a.Representative.UniversalIP
        //         },
        //         RFQActionAtts = a.RFQActionAtts.Select(att => new RFQActionAtt
        //         {
        //             FileName = att.FileName,
        //             FileUrl = att.FileUrl,
        //             FileType = att.FileType,
        //             Value = att.Value
        //         }).ToList(),
        //         Id = a.Id,
        //         RFQId = a.RFQId,
        //         SubmissionTime = a.SubmissionTime,
        //         UniversalIP = a.UniversalIP
        //     }).SingleOrDefault(a => a.ActionTime == item.RFQActions.Max(a1 => a1.ActionTime));

        return await Task.Run(() => new ObjectResult(rfqActionDto));
    }

    // GET RFQ/Actions/5
    [HttpGet("{id}", Name = "GetRFQActions")]
    public async Task<IEnumerable<Object>> Actions(int id)
    {
        var item = _context.RFQs
            .Where(o => o.RFQId == id)
            .Include(r => r.RFQActions)
            .ThenInclude(a => a.Representative)
            .Include(r => r.RFQActions)
            .ThenInclude(a => a.RFQActionAtts)
            .FirstOrDefault();

        if (item == null)
        {
            return null;
        }

        var rfqActionsDto = _mapper.Map<IEnumerable<RFQActionDTO>>(item.RFQActions);
        // var rfqActions = item.RFQActions
        //     .Select(a => new RFQAction
        //     {
        //         ActionCode = a.ActionCode,
        //         ActionTime = a.ActionTime,
        //         ActionType = a.ActionType,
        //         Comments = a.Comments,
        //         RepresentativeId = a.RepresentativeId,
        //         Representative = new Representative
        //         {
        //             Address = a.Representative.Address,
        //             Continuous = a.Representative.Continuous,
        //             Created = a.Representative.Created,
        //             DateOfBirth = a.Representative.DateOfBirth,
        //             Id = a.Representative.Id,
        //             Name = a.Representative.Name,
        //             PersonalPhone = a.Representative.PersonalPhone,
        //             Phone = a.Representative.Phone,
        //             Position = a.Representative.Position,
        //             UniversalIP = a.Representative.UniversalIP
        //         },
        //         RFQActionAtts = a.RFQActionAtts.Select(att => new RFQActionAtt
        //         {
        //             FileName = att.FileName,
        //             FileUrl = att.FileUrl,
        //             FileType = att.FileType,
        //             Value = att.Value
        //         }).ToList(),
        //         Id = a.Id,
        //         RFQId = a.RFQId,
        //         SubmissionTime = a.SubmissionTime,
        //         UniversalIP = a.UniversalIP
        //     });

        return await Task.Run(() => rfqActionsDto);
    }

    // GET RFQ/Actions/5/1
    [HttpGet("{id}", Name = "GetRFQActionById")]
    public async Task<ActionResult> Action(int id, int actionId)
    {
        var item = _context.RFQs
            .Where(o => o.RFQId == id)
            .Include(r => r.RFQActions)
            .ThenInclude(a => a.Representative)
            .Include(r => r.RFQActions)
            .ThenInclude(a => a.RFQActionAtts)
            .FirstOrDefault();

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        var rfqActionsDto = _mapper.Map<RFQActionDTO>(item.RFQActions.SingleOrDefault(a => a.RFQId == id && a.Id == actionId));
        // var rfqAction = item.RFQActions
        //     .Select(a => new RFQAction
        //     {
        //         ActionCode = a.ActionCode,
        //         ActionTime = a.ActionTime,
        //         ActionType = a.ActionType,
        //         Comments = a.Comments,
        //         RepresentativeId = a.RepresentativeId,
        //         Representative = new Representative
        //         {
        //             Address = a.Representative.Address,
        //             Continuous = a.Representative.Continuous,
        //             Created = a.Representative.Created,
        //             DateOfBirth = a.Representative.DateOfBirth,
        //             Id = a.Representative.Id,
        //             Name = a.Representative.Name,
        //             PersonalPhone = a.Representative.PersonalPhone,
        //             Phone = a.Representative.Phone,
        //             Position = a.Representative.Position,
        //             UniversalIP = a.Representative.UniversalIP
        //         },
        //         RFQActionAtts = a.RFQActionAtts.Select(att => new RFQActionAtt
        //         {
        //             FileName = att.FileName,
        //             FileUrl = att.FileUrl,
        //             FileType = att.FileType,
        //             Value = att.Value
        //         }).ToList(),
        //         Id = a.Id,
        //         RFQId = a.RFQId,
        //         SubmissionTime = a.SubmissionTime,
        //         UniversalIP = a.UniversalIP
        //     }).SingleOrDefault(a => a.RFQId == id && a.Id == actionId);

        return await Task.Run(() => new ObjectResult(rfqActionsDto));
    }

    // POST RFQ/AddStatus/5
    [HttpPost("{id}", Name = "AddRFQAction")]
    public async Task<ActionResult> AddStatus(int id, [FromBody]RFQActionDTO actionDto)
    {
        var item = _context.RFQs
            .Where(o => o.RFQId == id)
            .Include(r => r.RFQActions)
            .ThenInclude(a => a.Representative)
            .Include(r => r.RFQActions)
            .ThenInclude(a => a.RFQActionAtts)
            .FirstOrDefault();

        if (item == null)
        {
            return NotFound();
        }

        actionDto.ActionCode = DateTime.Now.Ticks.ToString();
        actionDto.ActionTime = DateTime.Now;
        actionDto.SubmissionTime = DateTime.Now;

        var action = _mapper.Map<RFQAction>(actionDto);
        item.RFQActions.Add(action);
        _context.SaveChanges();

        // var retAction = new
        // {
        //     actionDto.ActionCode,
        //     actionDto.ActionTime,
        //     actionDto.ActionType,
        //     actionDto.Comments,
        //     actionDto.Id,
        //     actionDto.RepresentativeId,
        //     Representative = new
        //     {
        //         actionDto.Representative?.Address,
        //         actionDto.Representative?.Continuous,
        //         actionDto.Representative?.Created,
        //         actionDto.Representative?.DateOfBirth,
        //         actionDto.Representative?.Id,
        //         actionDto.Representative?.Name,
        //         actionDto.Representative?.PersonalPhone,
        //         actionDto.Representative?.Phone,
        //         actionDto.Representative?.Position,
        //         actionDto.Representative?.UniversalIP
        //     },
        //     RFQActionAtts = actionDto.RFQActionAtts?.Select(att => new
        //     {
        //         att.FileName,
        //         att.FileUrl,
        //         att.FileType,
        //         att.Value
        //     }),
        //     actionDto.RFQId,
        //     actionDto.SubmissionTime,
        //     actionDto.UniversalIP
        // };

        return await Task.Run(() => new ObjectResult(actionDto));
    }

    // PUT RFQ/UpdateStatus/5/1
    [HttpPut("{id}/{actionId}", Name = "UpdateRFQAction")]
    public async Task<ActionResult> UpdateStatus(int id, int actionId, [FromBody]RFQActionDTO actionDto)
    {
        var item = _context.RFQs.Where(o => o.RFQId == id).Include(r => r.RFQActions).ThenInclude(a => a.RFQActionAtts).FirstOrDefault();

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        var orgAction = item.RFQActions.Where(a => a.Id == actionId).FirstOrDefault();

        if (orgAction == null)
        {
            return await Task.Run(() => NotFound());
        }

        orgAction = _mapper.Map(actionDto, orgAction);
        // orgAction.ActionType = actionDto.ActionType;
        // orgAction.RepresentativeId = actionDto.RepresentativeId;
        // orgAction.Comments = actionDto.Comments;
        // orgAction.UniversalIP = actionDto.UniversalIP;

        foreach (var att in orgAction.RFQActionAtts.Reverse())
        {
            if (actionDto.RFQActionAtts.Count(at => actionId == att.RFQActionId && at.FileName == att.FileName) == 0)
            {
                orgAction.RFQActionAtts.Remove(att);
            }
        }

        foreach (var att in orgAction.RFQActionAtts)
        {
            if (orgAction.RFQActionAtts.Count(at => at.RFQActionId == actionId && at.FileName == att.FileName) == 0)
            {
                // if (System.IO.Directory.Exists())
                // {

                // }
                string url = AppDomain.CurrentDomain.BaseDirectory + $"wwwroot\\Att\\{ id }\\{ actionId }" + att.FileName;
                // att.FileUrl = url;
                // System.IO.File.WriteAllBytes(url, Convert.FromBase64String(att.Value));
                orgAction.RFQActionAtts.Add(att);
            }
        }

        orgAction.SubmissionTime = DateTime.Now;
        _context.SaveChanges();

        // var retAction = new
        // {
        //     actionDto.ActionCode,
        //     actionDto.ActionTime,
        //     actionDto.ActionType,
        //     actionDto.Comments,
        //     actionDto.Id,
        //     actionDto.RepresentativeId,
        //     Representative = new
        //     {
        //         actionDto.Representative.Address,
        //         actionDto.Representative.Continuous,
        //         actionDto.Representative.Created,
        //         actionDto.Representative.DateOfBirth,
        //         actionDto.Representative.Id,
        //         actionDto.Representative.Name,
        //         actionDto.Representative.PersonalPhone,
        //         actionDto.Representative.Phone,
        //         actionDto.Representative.Position,
        //         actionDto.Representative.UniversalIP
        //     },
        //     RFQActionAtts = actionDto.RFQActionAtts?.Select(att => new
        //     {
        //         att.FileName,
        //         att.FileUrl,
        //         att.FileType,
        //         att.Value
        //     }),
        //     actionDto.RFQId,
        //     actionDto.SubmissionTime,
        //     actionDto.UniversalIP
        // };

        return await Task.Run(() => new ObjectResult(actionDto));
    }

    // POST RFQ/DeleteStatus/5/1
    [HttpDelete("{id}/{actionId}", Name = "DeleteRFQAction")]
    public async Task<ActionResult> DeleteStatus(int id, int actionId)
    {
        var item = _context.RFQs
            .Where(o => o.RFQId == id)
            .Include(r => r.RFQActions)
            .FirstOrDefault();

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        var action = item.RFQActions
            .Where(a => a.Id == actionId)
            .FirstOrDefault();

        if (action == null)
        {
            return await Task.Run(() => NotFound());
        }

        item.RFQActions.Remove(action);
        _context.SaveChanges();

        var actionDto = _mapper.Map<RFQActionDTO>(action);
        return await Task.Run(() => new ObjectResult(actionDto));
    }
    #endregion
}