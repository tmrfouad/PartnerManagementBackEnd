using System;
using System.Collections.Generic;
using System.Linq;
using PartnerManagement.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using PartnerManagement.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

[Route("[controller]")]
[EnableCors("AllowAnyOrigin")]
[Authorize]
public class InvoiceActivityController : Controller
{
    PartnerManagementContext _context;
    IMapper _mapper;

    public InvoiceActivityController(PartnerManagementContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET invoiceactivity/
    [HttpGet]
    public async Task<IEnumerable<InvoiceActivityDto>> Get()
    {
        var itemsDto = _mapper.Map<IEnumerable<InvoiceActivityDto>>(_context.InvoiceActivities.ToList());
        return await Task.Run(() => itemsDto);
    }

    // GET invoiceactivity/1
    [HttpGet("{id}")]
    public async Task<InvoiceActivityDto> GetById(int id)
    {
        var itemDto = _mapper.Map<InvoiceActivityDto>(_context.InvoiceActivities.SingleOrDefault(e => e.Id == id));
        return await Task.Run(() => itemDto);
    }

    // POST invoiceactivity/
    [HttpPost]
    public async Task<ActionResult> Post([FromBody]InvoiceActivityDto itemDto)
    {
        if (itemDto == null)
        {
            return await Task.Run(() => BadRequest());
        }

        itemDto.Created = DateTime.Now;

        var item = _mapper.Map<InvoiceActivity>(itemDto);
        _context.InvoiceActivities.Add(item);
        _context.SaveChanges();

        itemDto = _mapper.Map<InvoiceActivityDto>(item);
        return await Task.Run(() => new ObjectResult(itemDto));
    }

    // PUT invoiceactivity/1
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody]InvoiceActivityDto itemDto)
    {
        if (itemDto == null)
        {
            return await Task.Run(() => BadRequest());
        }

        var item = _context.InvoiceActivities
            .SingleOrDefault(e => e.Id == id);

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        item = _mapper.Map(itemDto, item);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(itemDto));
    }

    // PUT invoiceactivity/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var item = _context.InvoiceActivities
            .SingleOrDefault(e => e.Id == id);

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        var itemDto = _mapper.Map<InvoiceActivityDto>(item);
        _context.InvoiceActivities.Remove(item);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(itemDto));
    }
}