
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models;
using ProductApi.Dtos;


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
    public async Task<ActionResult<IEnumerable<ProductItem>>> GetProducts()
    {
        var products = await _context.ProductItems
            .Include(pi => pi.ProductGroup)
            .Include(pi => pi.ProductSubGroup)
            .ToListAsync();

        return products;
    }

    // GET: api/products/1
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductItem>> GetProductById(int id)
    {
        var product = await _context.ProductItems
            .Include(pi => pi.ProductGroup)
            .Include(pi => pi.ProductSubGroup)
            .FirstOrDefaultAsync(pi => pi.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }


    [HttpPost("/api/search")]
    public IActionResult Search([FromBody] ProducSearchDto searchDto)
    {
        var query = _context.ProductItems
                            .Include(pi => pi.ProductGroup)
                            .Include(pi => pi.ProductSubGroup)
                            .Where(pi => pi.SoftDelete != true);

        if (!string.IsNullOrWhiteSpace(searchDto.name))
        {
            query = query.Where(pi => pi.Name.Contains(searchDto.name));
        }

        if (searchDto.groupId != null)
        {
            query = query.Where(pi => pi.ProductGroup.Id == searchDto.groupId);
        }

        if (searchDto.subGroupId != null)
        {
            query = query.Where(pi => pi.ProductSubGroup.Id == searchDto.subGroupId);
        }

        if (searchDto.isActive != null)
        {
            query = query.Where(pi => pi.IsActive == searchDto.isActive);
        }

        var products = query.ToList();

        return Ok(products);
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
    public async Task<IActionResult> UpdateProductStatusAsync(int id, [FromBody] ProductStatusUpdateDto statusDto)
    {
        var product = _context.ProductItems.Find(id);

        if (product == null)
        {
            return NotFound();
        }

        product.IsActive = statusDto.IsActive;
        if (product.SoftDelete)
        {
            product.SoftDelete = false;
        }

        _context.SaveChanges();

        var products = await _context.ProductItems
            .Include(pi => pi.ProductGroup)
            .Include(pi => pi.ProductSubGroup)
            .ToListAsync();

        return Ok(products);
    }

    [HttpPatch("{id}/soft-delete")]
    public async Task<IActionResult> SoftDeleteProductStatusAsync(int id)
    {
        var product = _context.ProductItems.Find(id);

        if (product == null)
        {
            return NotFound();
        }

        product.SoftDelete = true;

        _context.SaveChanges();

        var products = await _context.ProductItems
            .Include(pi => pi.ProductGroup)
            .Include(pi => pi.ProductSubGroup)
            .ToListAsync();

        return Ok(products);
    }


    // GET: api/Group
    [HttpGet("/api/Groups")]
    public async Task<ActionResult<IEnumerable<ProductGroup>>> GetGroups()
    {
        var groups = await _context.productGroups.ToListAsync();
        return groups;
    }


    // GET: api/SubGroup
    [HttpGet("/api/SubGroups")]
    public async Task<ActionResult<IEnumerable<ProductSubGroup>>> GetSubGroups()
    {
        var subGroups = await _context.productSubGroups
                                .Include(ps => ps.ProductGroup)
                                .ToListAsync();
        return subGroups;
    }

    [HttpGet("{groupId}/SubGroups")]
    public async Task<ActionResult<IEnumerable<ProductSubGroup>>> GetSubGroupsByGroup(int groupId)
    {
        var subGroups = await _context.productSubGroups
            .Where(psg => psg.ProductGroupId == groupId)
            .ToListAsync();

        if (subGroups == null)
        {
            return NotFound();
        }

        return subGroups;
    }

}
