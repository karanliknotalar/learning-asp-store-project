using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.ProductId);
        builder.Property(p => p.ProductName).IsRequired();
        builder.Property(p => p.Price).IsRequired();

        builder.HasData(
            new Product() { ProductId = 1, CategoryId = 2, ProductName = "Asus Computer", Price = 11_000 },
            new Product() { ProductId = 2, CategoryId = 2, ProductName = "Hp Notebook", Price = 12_000 },
            new Product() { ProductId = 3, CategoryId = 2, ProductName = "Iphone 13", Price = 13_000 },
            new Product() { ProductId = 4, CategoryId = 2, ProductName = "Iphone 14", Price = 14_000 },
            new Product() { ProductId = 5, CategoryId = 2, ProductName = "Iphone 15", Price = 15_000 },
            new Product() { ProductId = 6, CategoryId = 2, ProductName = "Iphone 16", Price = 16_000 },
            new Product() { ProductId = 7, CategoryId = 1, ProductName = "Angry", Price = 150 },
            new Product() { ProductId = 8, CategoryId = 1, ProductName = "Black And Dark", Price = 250 }
        );
    }
}