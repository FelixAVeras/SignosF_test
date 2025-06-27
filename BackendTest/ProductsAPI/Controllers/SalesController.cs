using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;
using ProductsAPI.Models.Data;

namespace ProductsAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly DataContext _context;

    public SalesController(DataContext context)
    {
        _context = context;
    }

    // GET: api/sales
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
    {
        return await _context.Sales.Include(s => s.Product).ToListAsync();
    }

    // GET: api/sales/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Sale>> GetSale(int id)
    {
        var sale = await _context.Sales.Include(s => s.Product)
            .FirstOrDefaultAsync(s => s.Id == id);
        
        if (sale == null)
        {
            return NotFound();
        }
        
        return sale;
    }
}