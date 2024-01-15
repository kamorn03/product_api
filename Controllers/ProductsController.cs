
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


    [HttpGet("{searchTerm}/search/{isActive}")]
    public IActionResult Search(string searchTerm, bool isActive)
    {
        var products = _context.ProductItems
                          .Include(pi => pi.ProductGroup)
                          .Include(pi => pi.ProductSubGroup)
                          .Where(pi => pi.Name.Contains(searchTerm) ||
                                        pi.ProductGroup.GroupName.Contains(searchTerm) ||
                                        pi.ProductSubGroup.SubGroupName.Contains(searchTerm))
                          .Where(pi => pi.IsActive == isActive)
                          .ToList();

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


    // GET: api/Group
     [HttpGet("/Groups")]
    public async Task<ActionResult<IEnumerable<ProductGroup>>> GetGroups()
    {
        var groups = await _context.productGroups.ToListAsync();
        return groups;
    }


    // GET: api/SubGroup
    [HttpGet("/SubGroups")]
    public async Task<ActionResult<IEnumerable<ProductSubGroup>>> GetSubGroups()
    {
        var subGroups = await _context.productSubGroups.ToListAsync();
        return subGroups;
    }

    [HttpGet("{subGroupId}/Groups")]
    public async Task<ActionResult<IEnumerable<ProductGroup>>> GetGroupsBySubGroup(int subGroupId)
    {
        var subGroups = _context.productSubGroups.Find(subGroupId);

        if (subGroups == null)
        {
            return NotFound();
        }
        var groups = await _context.productGroups
       .Where(psg => psg.Id == subGroups.ProductGroupId)
       .ToListAsync();

        return groups;
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
