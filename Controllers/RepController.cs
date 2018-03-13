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
    public ActionResult Post([FromBody]Representative Repitem)
    {
        try
        {
            if (Repitem == null)
                return NoContent();

            Repitem.SubmissionTime = DateTime.Now;
            _db.Representatives.Add(Repitem);
            _db.SaveChanges();

            return new NoContentResult();
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
    public Representative get(int id)
    {
        try
        {
            var rep = _db.Representatives.SingleOrDefault(x => x.RepresentativeId == id);
            return rep;
        }
        catch
        {
            throw;
        }
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, Representative representative)
    {
        try
        {
            var rep = _db.Representatives.Find(id);

            if (id != representative.RepresentativeId)
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
            rep.SubmissionTime = DateTime.Now;
            rep.UniversalIP = representative.UniversalIP;
            _db.SaveChanges();
            return new ObjectResult(rep);
        }
        catch
        {
            throw;
        }

    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        try
        {
            var representative = _db.Representatives.Find(id);
            if (representative != null)
                return BadRequest();

            _db.Representatives.Remove(representative);
            _db.SaveChanges();

            return new NoContentResult();
        }
        catch
        {
            throw;
        }
    }
}