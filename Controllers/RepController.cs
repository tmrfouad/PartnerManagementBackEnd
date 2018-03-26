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

[Authorize]
[Route("[controller]/[action]")]
[EnableCors("AllowAnyOrigin")]

public class RepController : Controller
{
    CustomersGateContext _db;

    public RepController(CustomersGateContext context)
    {
        _db = context;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody]Representative Repitem)
    {
        try
        {
            if (Repitem == null)
                return NoContent();

        Repitem.Created = DateTime.Now;  
        _db.Representatives.Add(Repitem);
        _db.SaveChanges();

             return await Task.Run(() => new ObjectResult(Repitem));
        }
        catch
        {
            throw;
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public IEnumerable<Representative> get()
    {
        return _db.Representatives.ToList();
    }

    [HttpGet("{id}")]
    public async Task<Representative> get(int id)
    {
        try
        {
            var rep = _db.Representatives.SingleOrDefault(x => x.Id == id);
            return await Task.Run(() => rep);
        }
        catch
        {
            throw;
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id,[FromBody] Representative representative)
    {
        try
        {
            var rep = _db.Representatives.Find(id);

            if (id != representative.Id)
                return BadRequest();

            if (rep == null)
                return BadRequest();

            rep.Address = representative.Address;
            rep.Continuous = representative.Continuous;
            rep.DateOfBirth = representative.DateOfBirth;
            rep.Name = representative.Name;
            rep.PersonalPhone = representative.PersonalPhone;
            rep.Phone = representative.Phone;
            rep.Position = representative.Position;
            rep.Created = DateTime.Now;
            rep.UniversalIP = representative.UniversalIP;
            _db.SaveChanges();
            return await Task.Run(() => new ObjectResult(rep));
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
            var representative = _db.Representatives.Find(id);
            if (representative == null)
                return BadRequest();

            _db.Representatives.Remove(representative);
            _db.SaveChanges();

            return await Task.Run(() => new NoContentResult());
        }
        catch
        {
            throw;
        }
    }
}