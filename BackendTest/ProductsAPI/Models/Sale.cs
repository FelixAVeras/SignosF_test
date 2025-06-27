namespace ProductsAPI.Models;

public class Sale
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime SaleDate { get; set; } = DateTime.UtcNow;

    // Navigation property for related product
    public Product? Product { get; set; }

    // Timestamp for when the sale was created
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}