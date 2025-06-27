namespace ProductsAPI.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }

    // Foreign key to Category
    public int CategoryId { get; set; }

    // Navigation property for related category
    public Category? Category { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
}