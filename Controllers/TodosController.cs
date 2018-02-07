using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class TodosController : Controller
{
    CustomersGateContext _context;

    public TodosController(CustomersGateContext context)
    {
        _context = context;

        if (_context.TodoItems.Count() == 0)
        {
            _context.TodoItems.Add(new TodoItem{ Id = 1, Name = "Todo Item 1", IsComplete = false });
            _context.SaveChanges();
        }
    }

        // GET api/Todos
        [HttpGet]
        public IEnumerable<TodoItem> Get()
        {
            return _context.TodoItems.ToList();
        }

        // GET api/Todos/5
        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult Get(int id)
        {
            var item = _context.TodoItems.SingleOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        // POST api/Todos
        [HttpPost]
        public ActionResult Post([FromBody]TodoItem item)
        {
            if (item == null)
            {
                BadRequest();
            }

            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        // PUT api/Todos/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]TodoItem item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var orgItem = _context.TodoItems.SingleOrDefault(t => t.Id == id);
            if (orgItem == null)
            {
                return NotFound();
            }

            orgItem.IsComplete = item.IsComplete;
            orgItem.Name = item.Name;

            _context.TodoItems.Update(orgItem);
            _context.SaveChanges();

            return new NoContentResult();
        }

        // DELETE api/Todos/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var item = _context.TodoItems.SingleOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(item);
            _context.SaveChanges();

            return new NoContentResult();
        }
}