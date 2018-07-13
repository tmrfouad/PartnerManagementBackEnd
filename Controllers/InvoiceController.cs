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
public class InvoiceController : Controller
{
    PartnerManagementContext _context;
    IMapper _mapper;

    public InvoiceController(PartnerManagementContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET invoice/
    [HttpGet]
    public async Task<IEnumerable<InvoiceDto>> Get()
    {
        var itemsDto = _mapper.Map<IEnumerable<InvoiceDto>>(_context.Invoices.ToList());
        return await Task.Run(() => itemsDto);
    }

    // GET invoice/1
    [HttpGet("{id}")]
    public async Task<InvoiceDto> GetById(int id)
    {
        var itemDto = _mapper.Map<InvoiceDto>(_context.Invoices.SingleOrDefault(e => e.Id == id));
        return await Task.Run(() => itemDto);
    }

    // POST invoice/
    [HttpPost]
    public async Task<ActionResult> Post([FromBody]InvoiceDto itemDto)
    {
        if (itemDto == null)
        {
            return await Task.Run(() => BadRequest());
        }

        itemDto.Created = DateTime.Now;

        var item = _mapper.Map<Invoice>(itemDto);
        _context.Invoices.Add(item);
        _context.SaveChanges();

        itemDto = _mapper.Map<InvoiceDto>(item);
        return await Task.Run(() => new ObjectResult(itemDto));
    }

    // PUT invoice/1
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody]InvoiceDto itemDto)
    {
        if (itemDto == null)
        {
            return await Task.Run(() => BadRequest());
        }

        var item = _context.Invoices
            .SingleOrDefault(e => e.Id == id);

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        item = _mapper.Map(itemDto, item);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(itemDto));
    }

    // PUT invoice/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var item = _context.Invoices
            .SingleOrDefault(e => e.Id == id);

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        var itemDto = _mapper.Map<InvoiceDto>(item);
        _context.Invoices.Remove(item);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(itemDto));
    }
}