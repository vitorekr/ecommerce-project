using CatalogService.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Data;

public class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Produto 1", Description = "Descrição do Produto 1", Price = 99.99m, StockQuantity = 10 },
            new Product { Id = 2, Name = "Produto 2", Description = "Descrição do Produto 2", Price = 149.99m, StockQuantity = 5 }
        );
    }
}
