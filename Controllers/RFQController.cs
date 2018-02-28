using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using acscustomersgatebackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Authorize]
[Route("api/[controller]")]
[EnableCors("AllowAnyOrigin")]
public class RFQController : Controller
{
    CustomersGateContext _context;

    public RFQController(CustomersGateContext context)
    {
        _context = context;
    }

    #region RFQs
    // GET api/RFQ
    [HttpGet]
    public IEnumerable<RFQ> Get()
    {
        return _context.RFQs.ToList();
    }

    // GET api/RFQ/5
    [HttpGet("{id}", Name = "GetRFQ")]
    public ActionResult Get(int id)
    {
        var item = _context.RFQs.SingleOrDefault(o => o.RFQId == id);
        if (item == null)
        {
            return NotFound();
        }

        return new ObjectResult(item);
    }

    // POST api/RFQ
    [HttpPost]
    [AllowAnonymous]
    public ActionResult Post([FromBody]RFQ rfq)
    {
        if (rfq == null)
        {
            BadRequest();
        }

        bool saved = false;

        try
        {
            rfq.RFQCode = DateTime.Now.Ticks.ToString();

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
            return new NoContentResult();

        bool sent = false;
        try
        {
            MailHelper.sendMail(new MailData
            {
                Message = new MailMessageData(new[] { rfq.ContactPersonEmail })
                {
                    Body = MailHelper.MessageBody(rfq.ContactPersonEnglishName, rfq.ContactPersonMobile)
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
                ActionCode = new Guid().ToString(),
                ActionTime = DateTime.Now,
                ActionType = ActionType.EmailMessage,
                Comments = "",
                CompanyRepresentative = "",
                SubmissionTime = DateTime.Now,
                UniversalIP = ""
            };
            rfq.RFQActions.Add(rfqAction);

            Put(rfq.RFQId, rfq);
        }

        return new ObjectResult(rfq);
    }

    // PUT api/RFQ/5
    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody]RFQ rfq)
    {
        if (rfq == null || rfq.RFQId != id)
        {
            return BadRequest();
        }

        var orgItem = _context.RFQs.SingleOrDefault(o => o.RFQId == id);
        if (orgItem == null)
        {
            return NotFound();
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
        orgItem.RFQActions = rfq.RFQActions;
        orgItem.RFQCode = rfq.RFQCode;
        orgItem.SelectedBundle = rfq.SelectedBundle;
        orgItem.Status = rfq.Status;
        orgItem.SubmissionTime = DateTime.Now;
        orgItem.TargetedProduct = rfq.TargetedProduct;
        orgItem.UniversalIP = "";
        orgItem.Website = rfq.Website;

        _context.RFQs.Update(orgItem);
        _context.SaveChanges();

        return new ObjectResult(orgItem);
    }

    // DELETE api/RFQ/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var item = _context.RFQs.SingleOrDefault(o => o.RFQId == id);
        if (item == null)
        {
            return NotFound();
        }

        _context.RFQs.Remove(item);
        _context.SaveChanges();

        return new NoContentResult();
    }
    #endregion
    #region Actions
    // GET api/RFQ/Status/5
    [HttpGet("[action]/{id}", Name = "GetRFQStatus")]
    public async Task<ActionResult> Status(int id)
    {
        var item = _context.RFQs
            .Where(o => o.RFQId == id)
            .Include(r => r.RFQActions)
            .FirstOrDefault();

        if (item == null)
        {
            return NotFound();
        }

        var rfqAction = item.RFQActions
            .Select(a =>
            {
                return new
                {
                    a.ActionCode,
                    a.ActionTime,
                    a.ActionType,
                    a.Comments,
                    a.CompanyRepresentative,
                    a.Id,
                    a.RFQId,
                    a.SubmissionTime,
                    a.UniversalIP
                };
            }).SingleOrDefault(a => a.ActionTime == item.RFQActions.Max(a1 => a1.ActionTime));

        return await Task.Run(() => new ObjectResult(rfqAction));
    }

    // GET api/RFQ/Actions/5
    [HttpGet("[action]/{id}", Name = "GetRFQActions")]
    public async Task<IEnumerable<Object>> Actions(int id)
    {
        var item = _context.RFQs
            .Where(o => o.RFQId == id)
            .Include(r => r.RFQActions)
            .FirstOrDefault();

        if (item == null)
        {
            return null;
        }

        var rfqActions = item.RFQActions
            .Select(a =>
            {
                return new
                {
                    a.ActionCode,
                    a.ActionTime,
                    a.ActionType,
                    a.Comments,
                    a.CompanyRepresentative,
                    a.Id,
                    a.RFQId,
                    a.SubmissionTime,
                    a.UniversalIP
                };
            });

        return await Task.Run(() => rfqActions);
    }

    // POST api/RFQ/AddStatus/5
    [HttpPost("[action]/{id}", Name = "AddRFQAction")]
    public async Task<ActionResult> AddStatus(int id, [FromBody]RFQAction action)
    {
        var item = _context.RFQs.Where(o => o.RFQId == id).Include(r => r.RFQActions).FirstOrDefault();

        if (item == null)
        {
            return NotFound();
        }

        action.ActionCode = DateTime.Now.Ticks.ToString();

        item.RFQActions.Add(action);
        _context.SaveChanges();

        return await Task.Run(() => new NoContentResult());
    }
    #endregion
}