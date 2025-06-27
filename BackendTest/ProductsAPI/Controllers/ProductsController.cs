using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;
using ProductsAPI.Models.Data;

namespace ProductsAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly DataContext _context;

    public ProductsController(DataContext context)
    {
        _context = context;
    }

    // GET: api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
    {
        var products = await _context.Products.Include(p => p.Category).Select(p => new ProductDTO
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Stock = p.Stock,
            CategoryName = p.Category!.Name
        }).ToListAsync();

    return Ok(products);
    }

    // GET: api/products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
        if (product == null)
        {
            return BadRequest("Product cannot be null.");
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    // PUT: api/products/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest("Product ID mismatch.");
        }

        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.Id == id);
    }

    // this function return filter products with stock > 0 and order them by price descending
    // GET: api/products/available
    [HttpGet("available")]
    public async Task<ActionResult<IEnumerable<Product>>> GetAvailableProducts()
    {
        var availableProducts = await _context.Products
            .Where(p => p.Stock > 0)
            .OrderByDescending(p => p.Price)
            .ToListAsync();

        if (availableProducts == null || !availableProducts.Any())
        {
            return NotFound("No available products found.");
        }

        return availableProducts;
    }

    // this function receives a JSON string, validates it, and returns an object with an additional field processed: true
    // POST: api/products/validate
    [HttpPost("validate")]
    public ActionResult<dynamic> ValidateJson([FromBody] string jsonString)
    {
        try
        {
            var jsonObject = System.Text.Json.JsonSerializer.Deserialize<dynamic>(jsonString);
            
            if (jsonObject == null)
            {
                return BadRequest("Invalid JSON format.");
            }

            // Add a processed field
            jsonObject["processed"] = true;

            return Ok(jsonObject);
        }
        catch (System.Text.Json.JsonException ex)
        {
            return BadRequest($"JSON validation error: {ex.Message}");
        }
    }

    // Simulate an asynchronous HTTP call that returns a product after 2 seconds
    // GET: api/products/simulate
    [HttpGet("simulate")]
    public async Task<ActionResult<Product>> SimulateAsyncProduct()
    {
        // Simulate a delay of 2 seconds
        await Task.Delay(2000);

        // Return a sample product
        var product = new Product
        {
            Id = 1,
            Name = "Sample Product",
            Description = "This is a sample product.",
            Price = 19.99m,
            Stock = 100,
            CreatedAt = DateTime.UtcNow,
            CategoryId = 1 // Assuming category with ID 1 exists
        };

        return Ok(product);
    }
}