using Repositories.Contracts;
using Entities.Models;
using System.Linq.Expressions;

namespace Repositories;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(RepositoryContext context)
        : base(context)
    {
    }

    public IQueryable<Product> GetAllProducts(bool trackChanges) => FindAll(trackChanges);

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