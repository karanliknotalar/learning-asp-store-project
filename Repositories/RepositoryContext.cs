using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class RepositoryContext : DbContext
{
    public DbSet<Product>? Products { get; set; }
    public DbSet<Category>? Categories { get; set; }

    public RepositoryContext(DbContextOptions<RepositoryContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
            .Entity<Product>()
            .HasData(
                new Product() { ProductId = 1, ProductName = "Computer", Price = 11_000 },
                new Product() { ProductId = 2, ProductName = "LAPTOP", Price = 12_000 },
                new Product() { ProductId = 3, ProductName = "IPHONE 13", Price = 13_000 },
                new Product() { ProductId = 4, ProductName = "IPHONE 14", Price = 14_000 },
                new Product() { ProductId = 5, ProductName = "IPHONE 15", Price = 15_000 },
                new Product() { ProductId = 6, ProductName = "IPHONE 16", Price = 15_000 }
            );

        modelBuilder
            .Entity<Category>()
            .HasData(
                new Category() { CategoryId = 1, CategoryName = "Book" },
                new Category() { CategoryId = 2, CategoryName = "Electronic" }
            );
    }
}