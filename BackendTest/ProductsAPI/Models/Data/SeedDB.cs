using Microsoft.EntityFrameworkCore;

namespace ProductsAPI.Models.Data;

public class SeedDatabase
{
    public static void Seed(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();

        // Only apply migrations if the provider is relational (SQL Server, PostgreSQL, etc.)
        var isRelational = context.Database.IsRelational();
        if (isRelational)
        {
            context.Database.Migrate(); // Apply automatic migrations
        }

        // Seed Categories
        if (!context.Categories.Any())
        {
            var categories = new[]
            {
                new Category { Name = "Bebidas", Description = "Bebidas frías y calientes" },
                new Category { Name = "Snacks", Description = "Botanas y aperitivos" },
                new Category { Name = "Frutas", Description = "Frutas frescas y naturales" },
                new Category { Name = "Carnes", Description = "Carnes rojas y blancas" },
                new Category { Name = "Lácteos", Description = "Productos derivados de la leche" }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        // Seed Products
        if (!context.Products.Any())
        {
            var products = new[]
            {
                new Product { Name = "Coca-Cola", Description = "Refresco de cola", Price = 1.5M, CategoryId = 1, Stock = 100 },
                new Product { Name = "Pepsi", Description = "Refresco de cola", Price = 1.4M, CategoryId = 1, Stock = 80 },
                new Product { Name = "Chips", Description = "Papas fritas sabor original", Price = 2.0M, CategoryId = 2, Stock = 150 },
                new Product { Name = "Manzana", Description = "Manzana roja fresca", Price = 0.5M, CategoryId = 3, Stock = 200 },
                new Product { Name = "Banana", Description = "Banana madura", Price = 0.3M, CategoryId = 3, Stock = 180 },
                new Product { Name = "Pollo", Description = "Pechuga de pollo", Price = 5.0M, CategoryId = 4, Stock = 90 },
                new Product { Name = "Res", Description = "Carne de res molida", Price = 7.0M, CategoryId = 4, Stock = 75 },
                new Product { Name = "Leche", Description = "Leche entera", Price = 1.2M, CategoryId = 5, Stock = 120 },
                new Product { Name = "Yogurt", Description = "Yogurt natural", Price = 1.0M, CategoryId = 5, Stock = 95 },
                new Product { Name = "Queso", Description = "Queso cheddar", Price = 2.5M, CategoryId = 5, Stock = 60 }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }

        // Seed Sales
        if (!context.Sales.Any())
        {
            var random = new Random();
            var productIds = context.Products.Select(p => p.Id).ToList();

            var sales = new List<Sale>();
            for (int i = 0; i < 20; i++)
            {
                sales.Add(new Sale
                {
                    ProductId = productIds[random.Next(productIds.Count)],
                    Quantity = random.Next(1, 10),
                    SaleDate = DateTime.UtcNow.AddDays(-random.Next(1, 60)),
                    CreatedAt = DateTime.UtcNow
                });
            }

            context.Sales.AddRange(sales);
            context.SaveChanges();
        }
    }
}