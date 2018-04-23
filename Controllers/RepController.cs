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
using AutoMapper;
using PartnerManagement.Models.DTOs;

[Authorize]
[Route("[controller]/[action]")]
[EnableCors("AllowAnyOrigin")]

public class RepController : Controller
{
    PartnerManagementContext _context;
    IMapper _mapper;

    public RepController(PartnerManagementContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody]RepresentativeDTO repDto)
    {
        try
        {
            if (repDto == null)
                return NoContent();

            repDto.Created = DateTime.Now;
            var rep = _mapper.Map<Representative>(repDto);
            _context.Representatives.Add(rep);
            _context.SaveChanges();

            repDto = _mapper.Map<RepresentativeDTO>(rep);
            return await Task.Run(() => new ObjectResult(repDto));
        }
        catch
        {
            throw;
        }
    }

    [HttpGet]
    public async Task<IEnumerable<RepresentativeDTO>> Get()
    {
        var items = _mapper.Map<IEnumerable<RepresentativeDTO>>(_context.Representatives.ToList());
        return await Task.Run(() => items);
    }

    [HttpGet("{id}")]
    public async Task<RepresentativeDTO> Get(int id)
    {
        try
        {
            var repDto = _mapper.Map<RepresentativeDTO>(_context.Representatives.SingleOrDefault(x => x.Id == id));
            return await Task.Run(() => repDto);
        }
        catch
        {
            throw;
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] RepresentativeDTO repDto)
    {
        try
        {
            var orgRep = _context.Representatives.Find(id);

            if (id != repDto.Id)
                return BadRequest();

            if (orgRep == null)
                return BadRequest();

            // rep.Address = repDto.Address;
            // rep.Continuous = repDto.Continuous;
            // rep.DateOfBirth = repDto.DateOfBirth;
            // rep.Name = repDto.Name;
            // rep.PersonalPhone = repDto.PersonalPhone;
            // rep.Phone = repDto.Phone;
            // rep.Position = repDto.Position;
            // rep.Created = DateTime.Now;
            // rep.UniversalIP = repDto.UniversalIP;
            // rep.Email = repDto.Email;

            orgRep = _mapper.Map(repDto, orgRep);
            _context.SaveChanges();
            return await Task.Run(() => new ObjectResult(repDto));
        }
        catch
        {
            throw;
        }

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var rep = _context.Representatives.Find(id);
            if (rep == null)
                return BadRequest();

            _context.Representatives.Remove(rep);
            _context.SaveChanges();

            var repDto = _mapper.Map<RepresentativeDTO>(rep);
            return await Task.Run(() => new ObjectResult(repDto));
        }
        catch
        {
            throw;
        }
    }
}