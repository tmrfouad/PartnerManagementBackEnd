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
public class SubscriptionUserController : Controller
{
    PartnerManagementContext _context;
    IMapper _mapper;

    public SubscriptionUserController(PartnerManagementContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET subscriptionuser/
    [HttpGet]
    public async Task<IEnumerable<SubscriptionUserDto>> Get()
    {
        var itemsDto = _mapper.Map<IEnumerable<SubscriptionUserDto>>(_context.SubscriptionUsers.ToList());
        return await Task.Run(() => itemsDto);
    }

    // GET subscriptionuser/1
    [HttpGet("{id}")]
    public async Task<SubscriptionUserDto> GetById(int id)
    {
        var itemDto = _mapper.Map<SubscriptionUserDto>(_context.SubscriptionUsers.SingleOrDefault(e => e.Id == id));
        return await Task.Run(() => itemDto);
    }

    // POST subscriptionuser/
    [HttpPost]
    public async Task<ActionResult> Post([FromBody]SubscriptionUserDto itemDto)
    {
        if (itemDto == null)
        {
            return await Task.Run(() => BadRequest());
        }

        itemDto.Created = DateTime.Now;

        var item = _mapper.Map<SubscriptionUser>(itemDto);
        _context.SubscriptionUsers.Add(item);
        _context.SaveChanges();

        itemDto = _mapper.Map<SubscriptionUserDto>(item);
        return await Task.Run(() => new ObjectResult(itemDto));
    }

    // PUT subscriptionuser/1
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody]SubscriptionUserDto itemDto)
    {
        if (itemDto == null)
        {
            return await Task.Run(() => BadRequest());
        }

        var item = _context.SubscriptionUsers
            .SingleOrDefault(e => e.Id == id);

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        item = _mapper.Map(itemDto, item);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(itemDto));
    }

    // PUT subscriptionuser/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var item = _context.SubscriptionUsers
            .SingleOrDefault(e => e.Id == id);

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        var itemDto = _mapper.Map<SubscriptionUserDto>(item);
        _context.SubscriptionUsers.Remove(item);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(itemDto));
    }
}