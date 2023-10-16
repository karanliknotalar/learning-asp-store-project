using Entities.Models;
using Entities.RequestParameters;

namespace Repositories.Contracts;

public interface IProductRepository : IRepositoryBase<Product>
{
    IQueryable<Product> GetAllProducts(bool trackChanges);
    IQueryable<Product> GetAllProductsWithDetails(ProductRequestParameters? parameters);
    IQueryable<Product> GetShowCaseProduct(bool trackChanges);
    Product? GetOneProduct(int id, bool trackChanges);
    void CreateProduct(Product product);
    void DeleteProduct(Product product);
    void UpdateProduct(Product product);
}