using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class OrdersController : Controller
{
    CustomersGateContext _context;

    public OrdersController(CustomersGateContext context)
    {
        _context = context;
    }

    // GET api/Orders
    [HttpGet]
    public IEnumerable<Order> Get()
    {
        return _context.Orders.ToList();
    }

    // GET api/Orders/5
    [HttpGet("{id}", Name = "GetOrder")]
    public ActionResult Get(int id)
    {
        var item = _context.Orders.SingleOrDefault(o => o.merchant_order_id == id);
        if (item == null)
        {
            return NotFound();
        }

        return new ObjectResult(item);
    }

    // POST api/Orders
    [HttpPost]
    public ActionResult Post([FromBody]Order order)
    {
        if (order == null)
        {
            BadRequest();
        }

        _context.Orders.Add(order);
        _context.SaveChanges();
        MailHelper.sendMail(new MailData {  
                              Message = new MailMessageData(new [] {order.shipping_data.email}) 
                              { Body = MailHelper.MessageBody(order.shipping_data.first_name+ " " + 
                                                              order.shipping_data.last_name ,
                                                              order.shipping_data.phone_number  )} , 
                              SMTP = new SmtpData() });

        return new NoContentResult();
    }

    // PUT api/Orders/5
    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody]Order order)
    {
        if (order == null || order.merchant_order_id != id)
        {
            return BadRequest();
        }

        var orgItem = _context.Orders.SingleOrDefault(o => o.merchant_order_id == id);
        if (orgItem == null)
        {
            return NotFound();
        }

        orgItem.amount_cents = order.amount_cents;
        orgItem.currency = order.currency;
        orgItem.delivery_needed = order.delivery_needed;
        orgItem.items = order.items;
        orgItem.merchant_id = order.merchant_id;
        orgItem.shipping_data = order.shipping_data;

        _context.Orders.Update(orgItem);
        _context.SaveChanges();

        return new NoContentResult();
    }

    // DELETE api/Orders/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var item = _context.Orders.SingleOrDefault(o => o.merchant_order_id == id);
        if (item == null)
        {
            return NotFound();
        }

        _context.Orders.Remove(item);
        _context.SaveChanges();

        return new NoContentResult();
    }
        
}