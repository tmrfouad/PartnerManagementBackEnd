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
using System.Net.Mail;
using System.Net;

[Route("[controller]/[action]")]
[EnableCors("AllowAnyOrigin")]
[Authorize]
public class MailController : Controller
{
    CustomersGateContext _context;

    public MailController(CustomersGateContext context)
    {
        _context = context;
    }

    // GET mail/get
    [HttpGet]
    public async Task<IEnumerable<EmailTemplate>> Get()
    {
        return await Task.Run(() => _context.EmailTemplates.ToList());
    }

    // GET mail/getById/1
    [HttpGet("{id}")]
    public async Task<EmailTemplate> GetById(int id)
    {
        return await Task.Run(() => _context.EmailTemplates
            .SingleOrDefault(e => e.Id == id));
    }

    // POST mail/post
    [HttpPost]
    public async Task<ActionResult> Post([FromBody]EmailTemplate template)
    {
        if (template == null)
        {
            return await Task.Run(() => BadRequest());
        }

        template.Created = DateTime.Now;
        _context.EmailTemplates.Add(template);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(template));
    }

    // POST mail/send
    [HttpPost]
    public async Task<ActionResult> Send([FromBody]MailData mail)
    {
        if (mail == null)
        {
            await Task.Run(() => BadRequest());
        }

        MailHelper.sendMail(mail);

        return await Task.Run(() => new ObjectResult(mail));
    }

    // PUT mail/put/1
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody]EmailTemplate template)
    {
        if (template == null)
        {
            return await Task.Run(() => BadRequest());
        }

        EmailTemplate orgTemplate = _context.EmailTemplates
            .SingleOrDefault(e => e.Id == id);

        if (orgTemplate == null)
        {
            return await Task.Run(() => NotFound());
        }

        orgTemplate.HtmlTemplate = template.HtmlTemplate;
        orgTemplate.Subject = template.Subject;
        orgTemplate.UniversalIP = template.UniversalIP;

        _context.EmailTemplates.Update(orgTemplate);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(orgTemplate));
    }

    // PUT mail/delete/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        EmailTemplate template = _context.EmailTemplates
            .SingleOrDefault(e => e.Id == id);

        if (template == null)
        {
            return await Task.Run(() => NotFound());
        }

        _context.EmailTemplates.Remove(template);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(template));
    }
}