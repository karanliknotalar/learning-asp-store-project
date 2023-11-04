using Repositories.Contracts;
using Entities.Models;
using Entities.RequestParameters;
using Microsoft.EntityFrameworkCore;
using Repositories.Extensions;

namespace Repositories;

public sealed class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(RepositoryContext context)
        : base(context)
    {
    }

    public IQueryable<Product> GetAllProducts(bool trackChanges) => FindAll(trackChanges);

    public IQueryable<Product> GetAllProductsWithDetails(ProductRequestParameters? param)
    {
        return _context.Products?
            .FilteredByCategoryId(param?.CategoryId)
            .FilteredBySearchTerm(param?.SearchTerm)
            .FilteredByPrice(param?.MinPrice, param?.MaxPrice, param!.IsValidPrice)
            .ToPaginate(param.PageNumber, param.PageSize)!;
    }

    public IQueryable<Product>? GetAllProductsWithCategories(bool trackChanges)
    {
        return _context.Products?
            .Include(p => p.Category)
            .Select(p => new Product
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                CategoryId = p.CategoryId,
                Summary = p.Summary,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                ShowCase = p.ShowCase,
                Category = new Category
                {
                    CategoryId = p.Category!.CategoryId,
                    CategoryName = p.Category!.CategoryName
                }
            });
    }

    public IQueryable<Product> GetShowCaseProduct(bool trackChanges)
    {
        return FindAll(trackChanges)
            .Where(p => p.ShowCase.Equals(true));
    }

    public Product? GetOneProduct(int id, bool trackChanges)
    {
        return FindByCondition(p => p.ProductId.Equals(id), trackChanges);
    }

    public void CreateProduct(Product product) => Create(product);
    public void DeleteProduct(Product product) => Delete(product);
    public void UpdateProduct(Product product) => Update(product);
}