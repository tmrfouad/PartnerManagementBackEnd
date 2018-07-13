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
public class InvoiceDetailController : Controller
{
    PartnerManagementContext _context;
    IMapper _mapper;

    public InvoiceDetailController(PartnerManagementContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET invoicedetail/
    [HttpGet]
    public async Task<IEnumerable<InvoiceDetailDto>> Get()
    {
        var itemsDto = _mapper.Map<IEnumerable<InvoiceDetailDto>>(_context.InvoiceDetails.ToList());
        return await Task.Run(() => itemsDto);
    }

    // GET invoicedetail/1
    [HttpGet("{id}")]
    public async Task<InvoiceDetailDto> GetById(int id)
    {
        var itemDto = _mapper.Map<InvoiceDetailDto>(_context.InvoiceDetails.SingleOrDefault(e => e.Id == id));
        return await Task.Run(() => itemDto);
    }

    // POST invoicedetail/
    [HttpPost]
    public async Task<ActionResult> Post([FromBody]InvoiceDetailDto itemDto)
    {
        if (itemDto == null)
        {
            return await Task.Run(() => BadRequest());
        }

        itemDto.Created = DateTime.Now;

        var item = _mapper.Map<InvoiceDetail>(itemDto);
        _context.InvoiceDetails.Add(item);
        _context.SaveChanges();

        itemDto = _mapper.Map<InvoiceDetailDto>(item);
        return await Task.Run(() => new ObjectResult(itemDto));
    }

    // PUT invoicedetail/1
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody]InvoiceDetailDto itemDto)
    {
        if (itemDto == null)
        {
            return await Task.Run(() => BadRequest());
        }

        var item = _context.InvoiceDetails
            .SingleOrDefault(e => e.Id == id);

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        item = _mapper.Map(itemDto, item);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(itemDto));
    }

    // PUT invoicedetail/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var item = _context.InvoiceDetails
            .SingleOrDefault(e => e.Id == id);

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        var itemDto = _mapper.Map<InvoiceDetailDto>(item);
        _context.InvoiceDetails.Remove(item);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(itemDto));
    }
}