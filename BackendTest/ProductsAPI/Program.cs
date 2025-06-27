using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models.Data;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

try
{
    /*
        try to open the connection before configuring the DbContext
        This ensures that the SQL Server database is available
    */
    using (var testConnection = new SqlConnection(connectionString))
    {
        // Attempt to open the connection to SQL Server
        // If SQL Server is not available, an exception will be thrown
        testConnection.Open();
        testConnection.Close();

        builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(connectionString));
        Console.WriteLine("✅ SQL Server detectado. Usando base de datos real.");
    }
}
catch
{
    builder.Services.AddDbContext<DataContext>(options =>
        options.UseInMemoryDatabase("FallbackDB"));
    Console.WriteLine("⚠️ SQL Server no disponible. Usando base de datos en memoria.");
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Seed Database
SeedDatabase.Seed(app);

app.Run();
