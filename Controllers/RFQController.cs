using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using acscustomersgatebackend.Models;
using acscustomersgatebackend.Models.Enumerations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net;
using System.IO;

[Authorize]
[Route("[controller]/[action]")]
[EnableCors("AllowAnyOrigin")]
public class RFQController : Controller
{
    CustomersGateContext _context;

    public RFQController(CustomersGateContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    public string test()
    {
        return "API is working...";
    }

    #region RFQs
    // GET RFQ/Get
    [HttpGet]
    public async Task<IEnumerable<RFQ>> Get()
    {
        return await Task.Run(() => _context.RFQs
            .Include(r => r.TargetedProduct)
            .Include(r => r.SelectedEdition)
            .Select(r => new RFQ
            {
                Address = r.Address,
                CompanyArabicName = r.CompanyArabicName,
                CompanyEnglishName = r.CompanyEnglishName,
                ContactPersonArabicName = r.ContactPersonArabicName,
                ContactPersonEmail = r.ContactPersonEmail,
                ContactPersonEnglishName = r.ContactPersonEnglishName,
                ContactPersonMobile = r.ContactPersonMobile,
                ContactPersonPosition = r.ContactPersonPosition,
                Location = r.Location,
                PhoneNumber = r.PhoneNumber,
                RFQCode = r.RFQCode,
                RFQId = r.RFQId,
                SelectedEditionId = r.SelectedEditionId,
                SelectedEdition = new ProductEdition
                {
                    ArabicName = r.SelectedEdition.ArabicName,
                    Created = r.SelectedEdition.Created,
                    EnglishName = r.SelectedEdition.EnglishName,
                    Id = r.SelectedEdition.Id,
                    ProductId = r.SelectedEdition.ProductId,
                    UniversalIP = r.SelectedEdition.UniversalIP
                },
                Status = r.Status,
                SubmissionTime = r.SubmissionTime,
                TargetedProductId = r.TargetedProductId,
                TargetedProduct = new Product
                {
                    ArabicName = r.TargetedProduct.ArabicName,
                    Created = r.TargetedProduct.Created,
                    EnglishName = r.TargetedProduct.EnglishName,
                    Id = r.TargetedProduct.Id,
                    UniversalIP = r.TargetedProduct.UniversalIP
                },
                UniversalIP = r.UniversalIP,
                Website = r.Website
            })
            .ToList());
    }

    // GET RFQ/Get/5
    [HttpGet("{id}", Name = "GetRFQ")]
    public async Task<ActionResult> Get(int id)
    {
        var item = _context.RFQs
            .Include(r => r.TargetedProduct)
            .Include(r => r.SelectedEdition)
            .Select(r => new RFQ
            {
                Address = r.Address,
                CompanyArabicName = r.CompanyArabicName,
                CompanyEnglishName = r.CompanyEnglishName,
                ContactPersonArabicName = r.ContactPersonArabicName,
                ContactPersonEmail = r.ContactPersonEmail,
                ContactPersonEnglishName = r.ContactPersonEnglishName,
                ContactPersonMobile = r.ContactPersonMobile,
                ContactPersonPosition = r.ContactPersonPosition,
                Location = r.Location,
                PhoneNumber = r.PhoneNumber,
                RFQCode = r.RFQCode,
                RFQId = r.RFQId,
                SelectedEditionId = r.SelectedEditionId,
                SelectedEdition = new ProductEdition
                {
                    ArabicName = r.SelectedEdition.ArabicName,
                    Created = r.SelectedEdition.Created,
                    EnglishName = r.SelectedEdition.EnglishName,
                    Id = r.SelectedEdition.Id,
                    ProductId = r.SelectedEdition.ProductId,
                    UniversalIP = r.SelectedEdition.UniversalIP
                },
                Status = r.Status,
                SubmissionTime = r.SubmissionTime,
                TargetedProductId = r.TargetedProductId,
                TargetedProduct = new Product
                {
                    ArabicName = r.TargetedProduct.ArabicName,
                    Created = r.TargetedProduct.Created,
                    EnglishName = r.TargetedProduct.EnglishName,
                    Id = r.TargetedProduct.Id,
                    UniversalIP = r.TargetedProduct.UniversalIP
                },
                UniversalIP = r.UniversalIP,
                Website = r.Website
            })
        .SingleOrDefault(o => o.RFQId == id);

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        return await Task.Run(() => new ObjectResult(item));
    }

    // POST RFQ/Post
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> Post([FromBody]RFQ rfq)
    {
        if (rfq == null)
        {
            return await Task.Run(() => BadRequest());
        }

        bool saved = false;

        try
        {
            rfq.RFQCode = DateTime.Now.Ticks.ToString();
            rfq.SubmissionTime = DateTime.Now;

            _context.RFQs.Add(rfq);
            _context.SaveChanges();
            saved = true;
        }
        catch (System.Exception)
        {
            saved = false;
            throw;
        }

        if (!saved)
            return await Task.Run(() => new NoContentResult());

        if (rfq.SendEmail)
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
                string[] tagValues = new[] { rfq.ContactPersonEnglishName, rfq.ContactPersonMobile };
                MailHelper.sendMail(new MailData
                {
                    Message = new MailMessageData(new[] { rfq.ContactPersonEmail })
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
                    UniversalIP = rfq.UniversalIP
                };
                var newRfq = _context.RFQs.Where(r => r.RFQId == rfq.RFQId).Include(r => r.RFQActions).SingleOrDefault();
                newRfq.RFQActions.Add(rfqAction);
                _context.SaveChanges();
            }
        }

        var _rfq = _context.RFQs
            .Where(r => r.RFQId == rfq.RFQId)
            .Include(r => r.SelectedEdition)
            .Include(r => r.TargetedProduct)
            .SingleOrDefault();

        var retRfq = new RFQ
        {
            Address = _rfq.Address,
            CompanyArabicName = _rfq.CompanyArabicName,
            CompanyEnglishName = _rfq.CompanyEnglishName,
            ContactPersonArabicName = _rfq.ContactPersonArabicName,
            ContactPersonEmail = _rfq.ContactPersonEmail,
            ContactPersonEnglishName = _rfq.ContactPersonEnglishName,
            ContactPersonMobile = _rfq.ContactPersonMobile,
            ContactPersonPosition = _rfq.ContactPersonPosition,
            Location = _rfq.Location,
            PhoneNumber = _rfq.PhoneNumber,
            RFQCode = _rfq.RFQCode,
            RFQId = _rfq.RFQId,
            SelectedEdition = new ProductEdition
            {
                ArabicName = _rfq.SelectedEdition.ArabicName,
                Created = _rfq.SelectedEdition.Created,
                EnglishName = _rfq.SelectedEdition.EnglishName,
                Id = _rfq.SelectedEdition.Id,
                ProductId = _rfq.SelectedEdition.ProductId,
                UniversalIP = _rfq.SelectedEdition.UniversalIP
            },
            SelectedEditionId = _rfq.SelectedEditionId,
            Status = _rfq.Status,
            SubmissionTime = _rfq.SubmissionTime,
            TargetedProduct = new Product
            {
                ArabicName = _rfq.TargetedProduct.ArabicName,
                Created = _rfq.TargetedProduct.Created,
                EnglishName = _rfq.TargetedProduct.EnglishName,
                Id = _rfq.TargetedProduct.Id,
                UniversalIP = _rfq.TargetedProduct.UniversalIP,
            },
            TargetedProductId = _rfq.TargetedProductId,
            UniversalIP = _rfq.UniversalIP,
            Website = _rfq.Website
        };

        return await Task.Run(() => new ObjectResult(retRfq));
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
    public async Task<ActionResult> Put(int id, [FromBody]RFQ rfq)
    {
        if (rfq == null || rfq.RFQId != id)
        {
            return await Task.Run(() => BadRequest());
        }

        var orgItem = _context.RFQs.SingleOrDefault(o => o.RFQId == id);
        if (orgItem == null)
        {
            return await Task.Run(() => NotFound());
        }

        orgItem.Address = rfq.Address;
        orgItem.CompanyArabicName = rfq.CompanyArabicName;
        orgItem.CompanyEnglishName = rfq.CompanyEnglishName;
        orgItem.ContactPersonArabicName = rfq.ContactPersonArabicName;
        orgItem.ContactPersonEmail = rfq.ContactPersonEmail;
        orgItem.ContactPersonEnglishName = rfq.ContactPersonEnglishName;
        orgItem.ContactPersonMobile = rfq.ContactPersonMobile;
        orgItem.ContactPersonPosition = rfq.ContactPersonPosition;
        orgItem.Location = rfq.Location;
        orgItem.PhoneNumber = rfq.PhoneNumber;
        orgItem.SelectedEditionId = rfq.SelectedEditionId;
        orgItem.Status = rfq.Status;
        orgItem.SubmissionTime = DateTime.Now;
        orgItem.TargetedProductId = rfq.TargetedProductId;
        orgItem.UniversalIP = rfq.UniversalIP;
        orgItem.Website = rfq.Website;

        _context.RFQs.Update(orgItem);
        _context.SaveChanges();

        var _rfq = _context.RFQs
            .Where(r => r.RFQId == rfq.RFQId)
            .Include(r => r.SelectedEdition)
            .Include(r => r.TargetedProduct)
            .SingleOrDefault();

        var retRfq = new RFQ
        {
            Address = _rfq.Address,
            CompanyArabicName = _rfq.CompanyArabicName,
            CompanyEnglishName = _rfq.CompanyEnglishName,
            ContactPersonArabicName = _rfq.ContactPersonArabicName,
            ContactPersonEmail = _rfq.ContactPersonEmail,
            ContactPersonEnglishName = _rfq.ContactPersonEnglishName,
            ContactPersonMobile = _rfq.ContactPersonMobile,
            ContactPersonPosition = _rfq.ContactPersonPosition,
            Location = _rfq.Location,
            PhoneNumber = _rfq.PhoneNumber,
            RFQCode = _rfq.RFQCode,
            RFQId = _rfq.RFQId,
            SelectedEdition = new ProductEdition
            {
                ArabicName = _rfq.SelectedEdition.ArabicName,
                Created = _rfq.SelectedEdition.Created,
                EnglishName = _rfq.SelectedEdition.EnglishName,
                Id = _rfq.SelectedEdition.Id,
                ProductId = _rfq.SelectedEdition.ProductId,
                UniversalIP = _rfq.SelectedEdition.UniversalIP
            },
            SelectedEditionId = _rfq.SelectedEditionId,
            Status = _rfq.Status,
            SubmissionTime = _rfq.SubmissionTime,
            TargetedProduct = new Product
            {
                ArabicName = _rfq.TargetedProduct.ArabicName,
                Created = _rfq.TargetedProduct.Created,
                EnglishName = _rfq.TargetedProduct.EnglishName,
                Id = _rfq.TargetedProduct.Id,
                UniversalIP = _rfq.TargetedProduct.UniversalIP,
            },
            TargetedProductId = _rfq.TargetedProductId,
            UniversalIP = _rfq.UniversalIP,
            Website = _rfq.Website
        };

        return await Task.Run(() => new ObjectResult(retRfq));
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

        return await Task.Run(() => new ObjectResult(item));
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

        var rfqAction = item.RFQActions
            .Select(a => new RFQAction
            {
                ActionCode = a.ActionCode,
                ActionTime = a.ActionTime,
                ActionType = a.ActionType,
                Comments = a.Comments,
                RepresentativeId = a.RepresentativeId,
                Representative = new Representative
                {
                    Address = a.Representative.Address,
                    Continuous = a.Representative.Continuous,
                    Created = a.Representative.Created,
                    DateOfBirth = a.Representative.DateOfBirth,
                    Id = a.Representative.Id,
                    Name = a.Representative.Name,
                    PersonalPhone = a.Representative.PersonalPhone,
                    Phone = a.Representative.Phone,
                    Position = a.Representative.Position,
                    UniversalIP = a.Representative.UniversalIP
                },
                RFQActionAtts = a.RFQActionAtts.Select(att => new RFQActionAtt
                {
                    FileName = att.FileName,
                    FileUrl = att.FileUrl,
                    FileType = att.FileType,
                    Value = att.Value
                }).ToList(),
                Id = a.Id,
                RFQId = a.RFQId,
                SubmissionTime = a.SubmissionTime,
                UniversalIP = a.UniversalIP
            }).SingleOrDefault(a => a.ActionTime == item.RFQActions.Max(a1 => a1.ActionTime));

        return await Task.Run(() => new ObjectResult(rfqAction));
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

        var rfqActions = item.RFQActions
            .Select(a => new RFQAction
            {
                ActionCode = a.ActionCode,
                ActionTime = a.ActionTime,
                ActionType = a.ActionType,
                Comments = a.Comments,
                RepresentativeId = a.RepresentativeId,
                Representative = new Representative
                {
                    Address = a.Representative.Address,
                    Continuous = a.Representative.Continuous,
                    Created = a.Representative.Created,
                    DateOfBirth = a.Representative.DateOfBirth,
                    Id = a.Representative.Id,
                    Name = a.Representative.Name,
                    PersonalPhone = a.Representative.PersonalPhone,
                    Phone = a.Representative.Phone,
                    Position = a.Representative.Position,
                    UniversalIP = a.Representative.UniversalIP
                },
                RFQActionAtts = a.RFQActionAtts.Select(att => new RFQActionAtt
                {
                    FileName = att.FileName,
                    FileUrl = att.FileUrl,
                    FileType = att.FileType,
                    Value = att.Value
                }).ToList(),
                Id = a.Id,
                RFQId = a.RFQId,
                SubmissionTime = a.SubmissionTime,
                UniversalIP = a.UniversalIP
            });

        return await Task.Run(() => rfqActions);
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

        var rfqAction = item.RFQActions
            .Select(a => new RFQAction
            {
                ActionCode = a.ActionCode,
                ActionTime = a.ActionTime,
                ActionType = a.ActionType,
                Comments = a.Comments,
                RepresentativeId = a.RepresentativeId,
                Representative = new Representative
                {
                    Address = a.Representative.Address,
                    Continuous = a.Representative.Continuous,
                    Created = a.Representative.Created,
                    DateOfBirth = a.Representative.DateOfBirth,
                    Id = a.Representative.Id,
                    Name = a.Representative.Name,
                    PersonalPhone = a.Representative.PersonalPhone,
                    Phone = a.Representative.Phone,
                    Position = a.Representative.Position,
                    UniversalIP = a.Representative.UniversalIP
                },
                RFQActionAtts = a.RFQActionAtts.Select(att => new RFQActionAtt
                {
                    FileName = att.FileName,
                    FileUrl = att.FileUrl,
                    FileType = att.FileType,
                    Value = att.Value
                }).ToList(),
                Id = a.Id,
                RFQId = a.RFQId,
                SubmissionTime = a.SubmissionTime,
                UniversalIP = a.UniversalIP
            }).SingleOrDefault(a => a.RFQId == id && a.Id == actionId);

        return await Task.Run(() => new ObjectResult(rfqAction));
    }

    // POST RFQ/AddStatus/5
    [HttpPost("{id}", Name = "AddRFQAction")]
    public async Task<ActionResult> AddStatus(int id, [FromBody]RFQAction action)
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

        action.ActionCode = DateTime.Now.Ticks.ToString();
        action.ActionTime = DateTime.Now;
        action.SubmissionTime = DateTime.Now;

        item.RFQActions.Add(action);
        _context.SaveChanges();

        var retAction = new
        {
            action.ActionCode,
            action.ActionTime,
            action.ActionType,
            action.Comments,
            action.Id,
            action.RepresentativeId,
            Representative = new
            {
                action.Representative?.Address,
                action.Representative?.Continuous,
                action.Representative?.Created,
                action.Representative?.DateOfBirth,
                action.Representative?.Id,
                action.Representative?.Name,
                action.Representative?.PersonalPhone,
                action.Representative?.Phone,
                action.Representative?.Position,
                action.Representative?.UniversalIP
            },
            RFQActionAtts = action.RFQActionAtts?.Select(att => new
            {
                att.FileName,
                att.FileUrl,
                att.FileType,
                att.Value
            }),
            action.RFQId,
            action.SubmissionTime,
            action.UniversalIP
        };

        return await Task.Run(() => new ObjectResult(retAction));
    }

    // PUT RFQ/UpdateStatus/5/1
    [HttpPut("{id}/{actionId}", Name = "UpdateRFQAction")]
    public async Task<ActionResult> UpdateStatus(int id, int actionId, [FromBody]RFQAction action)
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

        orgAction.ActionType = action.ActionType;
        orgAction.RepresentativeId = action.RepresentativeId;
        orgAction.Comments = action.Comments;
        orgAction.UniversalIP = action.UniversalIP;

        foreach (var att in orgAction.RFQActionAtts.Reverse())
        {
            if (action.RFQActionAtts.Count(at => actionId == att.RFQActionId && at.FileName == att.FileName) == 0)
            {
                orgAction.RFQActionAtts.Remove(att);
            }
        }

        foreach (var att in action.RFQActionAtts)
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

        var retAction = new
        {
            action.ActionCode,
            action.ActionTime,
            action.ActionType,
            action.Comments,
            action.Id,
            action.RepresentativeId,
            Representative = new
            {
                action.Representative.Address,
                action.Representative.Continuous,
                action.Representative.Created,
                action.Representative.DateOfBirth,
                action.Representative.Id,
                action.Representative.Name,
                action.Representative.PersonalPhone,
                action.Representative.Phone,
                action.Representative.Position,
                action.Representative.UniversalIP
            },
            RFQActionAtts = action.RFQActionAtts?.Select(att => new
            {
                att.FileName,
                att.FileUrl,
                att.FileType,
                att.Value
            }),
            action.RFQId,
            action.SubmissionTime,
            action.UniversalIP
        };

        return await Task.Run(() => new ObjectResult(retAction));
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

        return await Task.Run(() => new ObjectResult(action));
    }
    #endregion
}