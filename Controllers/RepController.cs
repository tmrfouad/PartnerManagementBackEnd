
using System;
using System.Collections.Generic;
using acscustomersgatebackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

[Authorize]
[Route("[controller]/[action]")]
[EnableCors("AllowAnyOrigin")]

public class RepController : Controller
{
    CustomersGateContext _db ;

    public RepController(CustomersGateContext context)
    {
        _db = context;
    }

    [HttpPost]
    public ActionResult Post([FromBody]Representative Repitem) 
    {
        if(Repitem == null)
            return NoContent();

        Repitem.SubmissionTime = DateTime.Now;  
        _db.Representatives.Add(Repitem);
        _db.SaveChanges();

        return new NoContentResult();
    }

    [HttpGet]
    [AllowAnonymous]
    public IEnumerable<Representative> get() 
    {
        return _db.Representatives.ToList();
    }
}