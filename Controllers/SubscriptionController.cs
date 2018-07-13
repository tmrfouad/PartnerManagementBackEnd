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
public class SubscriptionController : Controller
{
    PartnerManagementContext _context;
    IMapper _mapper;

    public SubscriptionController(PartnerManagementContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET subscription/
    [HttpGet]
    public async Task<IEnumerable<SubscriptionDto>> Get()
    {
        var itemsDto = _mapper.Map<IEnumerable<SubscriptionDto>>(_context.Subscriptions.ToList());
        return await Task.Run(() => itemsDto);
    }

    // GET subscription/1
    [HttpGet("{id}")]
    public async Task<SubscriptionDto> GetById(int id)
    {
        var itemDto = _mapper.Map<SubscriptionDto>(_context.Subscriptions.SingleOrDefault(e => e.Id == id));
        return await Task.Run(() => itemDto);
    }

    // POST subscription/
    [HttpPost]
    public async Task<ActionResult> Post([FromBody]SubscriptionDto itemDto)
    {
        if (itemDto == null)
        {
            return await Task.Run(() => BadRequest());
        }

        itemDto.Created = DateTime.Now;

        var item = _mapper.Map<Subscription>(itemDto);
        _context.Subscriptions.Add(item);
        _context.SaveChanges();

        itemDto = _mapper.Map<SubscriptionDto>(item);
        return await Task.Run(() => new ObjectResult(itemDto));
    }

    // PUT subscription/1
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody]SubscriptionDto itemDto)
    {
        if (itemDto == null)
        {
            return await Task.Run(() => BadRequest());
        }

        var item = _context.Subscriptions
            .SingleOrDefault(e => e.Id == id);

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        item = _mapper.Map(itemDto, item);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(itemDto));
    }

    // PUT subscription/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var item = _context.Subscriptions
            .SingleOrDefault(e => e.Id == id);

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        var itemDto = _mapper.Map<SubscriptionDto>(item);
        _context.Subscriptions.Remove(item);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(itemDto));
    }
}