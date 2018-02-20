using System;
using System.Collections.Generic;
using System.Linq;
using acscustomersgatebackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[Route("api/[controller]")]
[EnableCors("AllowSpecificOrigin")]
public class RFQController : Controller
{
    CustomersGateContext _context;

    public RFQController(CustomersGateContext context)
    {
        _context = context;
    }

    // GET api/RFQs
    [HttpGet]
    public IEnumerable<RFQ> Get()
    {
        return _context.RFQs.ToList();
    }

    // GET api/RFQs/5
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

    // POST api/RFQs
    [HttpPost]
    public ActionResult Post([FromBody]RFQ rfq)
    {
        if (rfq == null)
        {
            BadRequest();
        }

        bool saved = false;

        try
        {
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
            RFQAction rfqAction = new RFQAction {
                ActionCode = new Guid().ToString(),
                ActionTime = DateTime.Now,
                ActionType = ActionType.EmailMessage,
                Comments = "",
                CompanyRepresentative = "",
                SubmissionTime = DateTime.Now,
                UniversalIP = ""
            };
            rfq.RFQActions = new List<RFQAction> { rfqAction };
            Put(rfq.RFQId, rfq);
        }

        return new NoContentResult();
    }

    // PUT api/RFQs/5
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

        return new NoContentResult();
    }

    // DELETE api/RFQs/5
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

}