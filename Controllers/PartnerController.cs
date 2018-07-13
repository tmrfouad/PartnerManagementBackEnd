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
public class PartnerController : Controller
{
    PartnerManagementContext _context;
    IMapper _mapper;

    public PartnerController(PartnerManagementContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET partner/
    [HttpGet]
    public async Task<IEnumerable<PartnerDto>> Get()
    {
        var itemsDto = _mapper.Map<IEnumerable<PartnerDto>>(_context.Partners.ToList());
        return await Task.Run(() => itemsDto);
    }

    // GET partner/1
    [HttpGet("{id}")]
    public async Task<PartnerDto> GetById(int id)
    {
        var itemDto = _mapper.Map<PartnerDto>(_context.Partners.SingleOrDefault(e => e.Id == id));
        return await Task.Run(() => itemDto);
    }

    // POST partner/
    [HttpPost]
    public async Task<ActionResult> Post([FromBody]PartnerDto itemDto)
    {
        if (itemDto == null)
        {
            return await Task.Run(() => BadRequest());
        }

        itemDto.Created = DateTime.Now;

        var item = _mapper.Map<Partner>(itemDto);
        _context.Partners.Add(item);
        _context.SaveChanges();

        itemDto = _mapper.Map<PartnerDto>(item);
        return await Task.Run(() => new ObjectResult(itemDto));
    }

    // PUT partner/1
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody]PartnerDto itemDto)
    {
        if (itemDto == null)
        {
            return await Task.Run(() => BadRequest());
        }

        var item = _context.Partners
            .SingleOrDefault(e => e.Id == id);

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        item = _mapper.Map(itemDto, item);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(itemDto));
    }

    // PUT partner/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Partner item = _context.Partners
            .SingleOrDefault(e => e.Id == id);

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        var itemDto = _mapper.Map<PartnerDto>(item);
        _context.Partners.Remove(item);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(itemDto));
    }
}