
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductAPIDbContext _context;

    public ProductsController(ProductAPIDbContext context)
    {
        _context = context;
    }

    // GET: api/products
    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = _context.ProductItems.ToList();
        return Ok(products);
    }

    // GET: api/products/1
    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var product = _context.ProductItems.Find(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    // POST: api/products
    [HttpPost]
    public IActionResult AddProduct(ProductItem product)
    {
        _context.ProductItems.Add(product);
        _context.SaveChanges();
        return Ok(product);
    }

    // PUT: api/products/1
    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, ProductItem updatedProduct)
    {
        var existingProduct = _context.ProductItems.Find(id);

        if (existingProduct == null)
        {
            return NotFound();
        }

        existingProduct.Name = updatedProduct.Name;
        existingProduct.IsActive = updatedProduct.IsActive;

        _context.SaveChanges();

        return Ok(existingProduct);
    }

    // DELETE: api/products/1
    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = _context.ProductItems.Find(id);

        if (product == null)
        {
            return NotFound();
        }

        _context.ProductItems.Remove(product);
        _context.SaveChanges();

        return NoContent();
    }

    // PATCH: api/products/1/update-status
    [HttpPatch("{id}/update-status")]
    public IActionResult UpdateProductStatus(int id, [FromBody] bool isActive)
    {
        var product = _context.ProductItems.Find(id);

        if (product == null)
        {
            return NotFound();
        }

        product.IsActive = isActive;

        _context.SaveChanges();

        return Ok(product);
    }
}
