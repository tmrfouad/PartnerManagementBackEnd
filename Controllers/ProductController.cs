using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using acscustomersgatebackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Authorize]
[Route("[controller]/[action]")]
[EnableCors("AllowAnyOrigin")]
public class ProductController : Controller
{
    CustomersGateContext _context;

    public ProductController(CustomersGateContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    public string test()
    {
        return "Product API is working...";
    }

    #region Products
    // GET Product/Get
    [HttpGet(Name = "GetAllProducts")]
    public async Task<IEnumerable<Product>> Get()
    {
        return await Task.Run(() => _context.Products.ToList());
    }

    // GET RFQ/Product/5
    [HttpGet("{id}", Name = "GetProductById")]
    public async Task<ActionResult> Get(int id)
    {
        var item = _context.Products.SingleOrDefault(o => o.Id == id);
        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        return await Task.Run(() => new ObjectResult(item));
    }

    // POST Product/Post
    [HttpPost(Name = "CreateProduct")]
    public async Task<ActionResult> Post([FromBody]Product product)
    {
        if (product == null)
        {
            return await Task.Run(() => BadRequest());
        }

        product.Created = DateTime.Now;

        _context.Products.Add(product);
        _context.SaveChanges();

        return await Task.Run(() => new NoContentResult());
    }

    // PUT Product/Put/5
    [HttpPut("{id}", Name = "UpdateProduct")]
    public async Task<ActionResult> Put(int id, [FromBody]Product product)
    {
        if (product == null || product.Id != id)
        {
            return await Task.Run(() => BadRequest());
        }

        var orgItem = _context.Products.SingleOrDefault(o => o.Id == id);
        if (orgItem == null)
        {
            return await Task.Run(() => NotFound());
        }

        orgItem.ArabicName = product.ArabicName;
        orgItem.EnglishName = product.EnglishName;
        orgItem.UniversalIP = product.UniversalIP;

        _context.Products.Update(orgItem);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(orgItem));
    }

    // DELETE Product/Delete/5
    [HttpDelete("{id}", Name = "DeleteProduct")]
    public async Task<ActionResult> Delete(int id)
    {
        var item = _context.Products.SingleOrDefault(o => o.Id == id);
        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        _context.Products.Remove(item);
        _context.SaveChanges();

        return await Task.Run(() => new NoContentResult());
    }
    #endregion

    #region ProductEditions
    // GET Product/Edidtion/5
    [HttpGet("{id}", Name = "GetAllProductEditions")]
    public async Task<IEnumerable<Object>> Editions(int id)
    {
        var item = _context.Products
            .Where(o => o.Id == id)
            .Include(r => r.ProductEditions)
            .FirstOrDefault();

        if (item == null)
        {
            return null;
        }

        var editions = item.ProductEditions
            .Select(e =>
            {
                return new
                {
                    e.ArabicName,
                    e.Created,
                    e.EnglishName,
                    e.Id,
                    e.ProductId,
                    e.UniversalIP
                };
            });

        return await Task.Run(() => editions);
    }
    
    // GET Product/Edidtions/5/1
    [HttpGet("{id}/{editionId}", Name = "GetProductEditionById")]
    public async Task<ActionResult> Editions(int id, int editionId)
    {
        var item = _context.Products
            .Where(o => o.Id == id)
            .Include(r => r.ProductEditions)
            .FirstOrDefault();

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        var edition = item.ProductEditions
            .Where(e => e.Id == editionId)
            .Select(e =>
            {
                return new
                {
                    e.ArabicName,
                    e.Created,
                    e.EnglishName,
                    e.Id,
                    e.ProductId,
                    e.UniversalIP
                };
            });

        return await Task.Run(() => new ObjectResult(edition));
    }

    // POST Product/AddEdition/5
    [HttpPost("{id}", Name = "AddProductEdition")]
    public async Task<ActionResult> AddEdition(int id, [FromBody]ProductEdition edition)
    {
        var item = _context.Products
            .Where(o => o.Id == id)
            .Include(r => r.ProductEditions)
            .FirstOrDefault();

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        edition.Created = DateTime.Now;

        item.ProductEditions.Add(edition);
        _context.SaveChanges();

        return await Task.Run(() => new NoContentResult());
    }

    // POST Product/UpdateEdition/5/1
    [HttpPost("{id}/{editionId}", Name = "UpdateProductEdition")]
    public async Task<ActionResult> UpdateEdition(int id, int editionId, [FromBody]ProductEdition edition)
    {
        var item = _context.Products
            .Where(o => o.Id == id)
            .Include(r => r.ProductEditions)
            .FirstOrDefault();

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        var orgEdition = item.ProductEditions
            .Where(a => a.Id == editionId)
            .FirstOrDefault();

        if (orgEdition == null)
        {
            return await Task.Run(() => NotFound());
        }

        orgEdition.ArabicName = edition.ArabicName;
        orgEdition.EnglishName = edition.EnglishName;
        orgEdition.UniversalIP = edition.UniversalIP;

        _context.SaveChanges();

        return await Task.Run(() => new NoContentResult());
    }

    // POST Product/DeleteEdition/5/1
    [HttpDelete("{id}/{editionId}", Name = "DeleteProductEdition")]
    public async Task<ActionResult> DeleteEdition(int id, int editionId)
    {
        var item = _context.Products
            .Where(o => o.Id == id)
            .Include(r => r.ProductEditions)
            .FirstOrDefault();

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        var edition = item.ProductEditions
            .Where(a => a.Id == editionId)
            .FirstOrDefault();

        if (edition == null)
        {
            return await Task.Run(() => NotFound());
        }

        item.ProductEditions.Remove(edition);
        _context.SaveChanges();

        return await Task.Run(() => new NoContentResult());
    }
    #endregion
}