using Entities.Models;

namespace Repositories.Contracts;

public interface IProductRepository : IRepositoryBase<Product>
{
    IEnumerable<Product> GetAllProducts(bool trackChanges);
    Product? GetOneProduct(int id, bool trackChanges);
}