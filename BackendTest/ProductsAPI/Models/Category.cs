namespace ProductsAPI.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    
    // Navigation property for related products
    public ICollection<Product> Products { get; set; } = new List<Product>();
    
    // Timestamp for when the category was created
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}