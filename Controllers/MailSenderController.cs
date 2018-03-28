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
using System.Net.Mail;
using System.Net;

[Route("[controller]/[action]")]
[EnableCors("AllowAnyOrigin")]
[Authorize]
public class MailSenderController : Controller
{
    CustomersGateContext _context;

    public MailSenderController(CustomersGateContext context)
    {
        _context = context;
    }

    // GET mail/get
    [HttpGet]
    public async Task<IEnumerable<EmailSender>> Get()
    {
        return await Task.Run(() => _context.EmailSenders.ToList());
    }

    // GET mail/getById/1
    [HttpGet("{id}")]
    public async Task<EmailSender> GetById(int id)
    {
        return await Task.Run(() => _context.EmailSenders
            .SingleOrDefault(e => e.Id == id));
    }

    // POST mail/post
    [HttpPost]
    public async Task<ActionResult> Post([FromBody]EmailSender sender)
    {
        if (sender == null)
        {
            return await Task.Run(() => BadRequest());
        }

        sender.Created = DateTime.Now;
        _context.EmailSenders.Add(sender);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(sender));
    }

    // PUT mail/put/1
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody]EmailSender sender)
    {
        if (sender == null)
        {
            return await Task.Run(() => BadRequest());
        }

        EmailSender orgSender = _context.EmailSenders
            .SingleOrDefault(e => e.Id == id);

        if (orgSender == null)
        {
            return await Task.Run(() => NotFound());
        }

        orgSender.Password = sender.Password;
        orgSender.Email = sender.Email;
        orgSender.UniversalIP = sender.UniversalIP;

        _context.EmailSenders.Update(orgSender);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(orgSender));
    }

    // PUT mail/delete/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        EmailSender sender = _context.EmailSenders
            .SingleOrDefault(e => e.Id == id);

        if (sender == null)
        {
            return await Task.Run(() => NotFound());
        }

        _context.EmailSenders.Remove(sender);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(sender));
    }
}