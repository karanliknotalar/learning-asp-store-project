using Entities.Models;

namespace Repositories.Extensions;

public static class ProductRepositoryExtension
{
    public static IQueryable<Product> FilteredByCategoryId(this IQueryable<Product> products, int? categoryId)
    {
        return categoryId.HasValue
            ? products
                // .Include(p => p.Category)
                .Where(p => p.CategoryId.Equals(categoryId))
            : products;
        // .Include(p => p.Category);
    }

    public static IQueryable<Product> FilteredBySearchTerm(this IQueryable<Product> products, string? query)
    {
        return string.IsNullOrWhiteSpace(query)
            ? products
            : products
                .Where(p => p.ProductName != null && p.ProductName.ToLower().Contains(query.ToLower()));
    }

    public static IQueryable<Product> FilteredByPrice(this IQueryable<Product> products, int? minPrice, int? maxPrice, bool isValidPrice)
    {
        return isValidPrice 
            ? products.Where(p => p.Price >= minPrice && p.Price <= maxPrice) 
            : products;
    }
}