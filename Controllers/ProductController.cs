using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PartnerManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using PartnerManagement.Models.DTOs;

[Authorize]
[Route("[controller]/[action]")]
[EnableCors("AllowAnyOrigin")]
public class ProductController : Controller
{
    CustomersGateContext _context;
    IMapper _mapper;

    public ProductController(CustomersGateContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [AllowAnonymous]
    public string test()
    {
        return "Product API is working...";
    }

    #region Products
    // GET Product/Get
    // GetAllProducts
    [HttpGet]
    public async Task<IEnumerable<ProductDTO>> Get()
    {
        var items = _mapper.Map<IEnumerable<ProductDTO>>(_context.Products.ToList());
        return await Task.Run(() => items);
    }

    // GET RFQ/Product/5
    // GetProductById
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var item = _context.Products.SingleOrDefault(o => o.Id == id);
        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }
        
        var itemDTO =  _mapper.Map<EmailSenderDTO>(item);
        return await Task.Run(() => new ObjectResult(itemDTO));
    }

    // POST Product/Post
    // CreateProduct
    [HttpPost]
    public async Task<ActionResult> Post([FromBody]ProductDTO productDto)
    {
        if (productDto == null)
        {
            return await Task.Run(() => BadRequest());
        }

        productDto.Created = DateTime.Now;

        var product = _mapper.Map<Product>(productDto);
        _context.Products.Add(product);
        _context.SaveChanges();

        productDto = _mapper.Map<ProductDTO>(product);
        return await Task.Run(() => new ObjectResult(productDto));
    }

    // PUT Product/Put/5
    // UpdateProduct
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody]ProductDTO productDto)
    {
        if (productDto == null || productDto.Id != id)
        {
            return await Task.Run(() => BadRequest());
        }

        var orgItem = _context.Products.SingleOrDefault(o => o.Id == id);
        if (orgItem == null)
        {
            return await Task.Run(() => NotFound());
        }

        orgItem = _mapper.Map(productDto, orgItem);
        _context.Products.Update(orgItem);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(productDto));
    }

    // DELETE Product/Delete/5
    // DeleteProduct
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var item = _context.Products.SingleOrDefault(o => o.Id == id);
        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        var delItem = _mapper.Map<ProductDTO>(item);
        _context.Products.Remove(item);
        _context.SaveChanges();

        return await Task.Run(() => new ObjectResult(delItem));
    }
    #endregion

    #region ProductEditions
    // GET Product/Edidtion/5
    // GetAllProductEditions
    [HttpGet("{id}")]
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

        var editions = _mapper.Map<IEnumerable<ProductEditionDTO>>(item.ProductEditions); 

        return await Task.Run(() => editions);
    }

    // GET Product/Edidtions/5/1
    // GetProductEditionById
    [HttpGet("{id}/{editionId}")]
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

        var edition = _mapper.Map<ProductEditionDTO>(item.ProductEditions.SingleOrDefault(e => e.Id == editionId));
        // var edition = item.ProductEditions
        //     .Where(e => e.Id == editionId)
        //     .Select(e =>
        //     {
        //         return new
        //         {
        //             e.ArabicName,
        //             e.Created,
        //             e.EnglishName,
        //             e.Id,
        //             e.ProductId,
        //             e.UniversalIP
        //         };
        //     });

        return await Task.Run(() => new ObjectResult(edition));
    }

    // POST Product/AddEdition/5
    // AddProductEdition
    [HttpPost("{id}")]
    public async Task<ActionResult> AddEdition(int id, [FromBody]ProductEditionDTO editionDto)
    {
        var item = _context.Products
            .Where(o => o.Id == id)
            .Include(r => r.ProductEditions)
            .FirstOrDefault();

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        editionDto.Created = DateTime.Now;

        var edition = _mapper.Map<ProductEdition>(editionDto);
        item.ProductEditions.Add(edition);
        _context.SaveChanges();

        editionDto = _mapper.Map<ProductEditionDTO>(edition);
        return await Task.Run(() => new ObjectResult(editionDto));
    }

    // POST Product/UpdateEdition/5/1
    // UpdateProductEdition
    [HttpPut("{id}/{editionId}")]
    public async Task<ActionResult> UpdateEdition(int id, int editionId, [FromBody]ProductEditionDTO editionDto)
    {
        try
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

            orgEdition = _mapper.Map(editionDto, orgEdition);
            _context.SaveChanges();

            return await Task.Run(() => new ObjectResult(editionDto));
        }
        catch(Exception ex)
         {
             var x = ex.Message;
            throw ;
        }

    }

    // POST Product/DeleteEdition/5/1
    // DeleteProductEdition
    [HttpDelete("{id}/{editionId}")]
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

        var editionDto = _mapper.Map<ProductEditionDTO>(edition);
        return await Task.Run(() => new ObjectResult(editionDto));
    }
    #endregion
}