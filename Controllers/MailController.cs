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
public class MailController : Controller
{
    CustomersGateContext _context;
    IMapper _mapper;

    public MailController(CustomersGateContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET mail/get
    [HttpGet]
    public async Task<IEnumerable<EmailTemplateDTO>> Get()
    {
        var items = _mapper.Map<IEnumerable<EmailTemplateDTO>>(_context.EmailTemplates.ToList());
        return await Task.Run(() => items);
    }

    // GET mail/getById/1
    [HttpGet("{id}")]
    public async Task<EmailTemplateDTO> GetById(int id)
    {
        var item = _mapper.Map<EmailTemplateDTO>(_context.EmailTemplates.SingleOrDefault(e => e.Id == id));
        return await Task.Run(() => item);
    }

    // POST mail/post
    [HttpPost]
    public async Task<ActionResult> Post([FromBody]EmailTemplateDTO templateDto)
    {
        if (templateDto == null)
        {
            return await Task.Run(() => BadRequest());
        }

        templateDto.Created = DateTime.Now;

        var template = _mapper.Map<EmailTemplate>(templateDto);
        _context.EmailTemplates.Add(template);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(templateDto));
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
    public async Task<ActionResult> Put(int id, [FromBody]EmailTemplateDTO templateDto)
    {
        if (templateDto == null)
        {
            return await Task.Run(() => BadRequest());
        }

        var orgTemplate = _context.EmailTemplates
            .SingleOrDefault(e => e.Id == id);

        if (orgTemplate == null)
        {
            return await Task.Run(() => NotFound());
        }

        orgTemplate = _mapper.Map(templateDto, orgTemplate);
        // orgTemplate.HtmlTemplate = templateDto.HtmlTemplate;
        // orgTemplate.Subject = templateDto.Subject;
        // orgTemplate.UniversalIP = templateDto.UniversalIP;
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(templateDto));
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

        var templateDto = _mapper.Map<EmailTemplateDTO>(template);
        return await Task.Run(() => new ObjectResult(templateDto));
    }
}