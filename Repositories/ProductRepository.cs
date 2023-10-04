using Repositories.Contracts;
using Entities.Models;
using System.Linq.Expressions;

namespace Repositories;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(RepositoryContext context)
        : base(context) { }

    public IQueryable<Product> GetAllProducts(bool trackChanges) => FindAll(trackChanges);

    public Product? GetOneProduct(int id, bool trackChanges)
    {
        return FindByCondition(p => p.ProductId.Equals(id), trackChanges);
    }
}
