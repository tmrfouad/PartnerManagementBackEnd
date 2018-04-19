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
using AutoMapper;
using PartnerManagement.Models.DTOs;

[Route("[controller]/[action]")]
[EnableCors("AllowAnyOrigin")]
[Authorize]
public class MailSenderController : Controller
{
    CustomersGateContext _context;
    IMapper _mapper;

    public MailSenderController(CustomersGateContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET mail/get
    [HttpGet]
    public async Task<IEnumerable<EmailSenderDTO>> Get()
    {
        var items = _mapper.Map<IEnumerable<EmailSenderDTO>>(_context.EmailSenders.ToList());
        return await Task.Run(() => items);
    }

    // GET mail/getById/1
    [HttpGet("{id}")]
    public async Task<EmailSenderDTO> GetById(int id)
    {
        var item = _mapper.Map<EmailSenderDTO>(_context.EmailSenders.SingleOrDefault(e => e.Id == id));
        return await Task.Run(() => item);
    }

    // POST mail/post
    [HttpPost]
    public async Task<ActionResult> Post([FromBody]EmailSenderDTO sender)
    {
        if (sender == null)
        {
            return await Task.Run(() => BadRequest());
        }

        sender.Created = DateTime.Now;

        var _sender = _mapper.Map<EmailSender>(sender);
        _context.EmailSenders.Add(_sender);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(sender));
    }

    // PUT mail/put/1
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody]EmailSenderDTO sender)
    {
        if (sender == null)
        {
            return await Task.Run(() => BadRequest());
        }

        var orgSender = _context.EmailSenders
            .SingleOrDefault(e => e.Id == id);

        if (orgSender == null)
        {
            return await Task.Run(() => NotFound());
        }

        orgSender = _mapper.Map(sender, orgSender);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(sender));
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

        var senderDto = _mapper.Map<EmailSenderDTO>(sender);
        _context.EmailSenders.Remove(sender);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(senderDto));
    }
}