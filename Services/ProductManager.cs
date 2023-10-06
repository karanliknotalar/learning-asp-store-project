using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ProductManager : IProductService
    {
        private readonly IRepositoryManager _manager;

        public ProductManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public IEnumerable<Product> GetAllProduct(bool trackChanges)
        {
            return _manager.Product.GetAllProducts(trackChanges);
        }

        public Product? GetOneProduct(int id, bool trackChanges)
        {
            return _manager.Product.GetOneProduct(id, trackChanges) ?? throw new Exception("Product not found!");
        }

        public void CreateProduct(Product product)
        {
            _manager.Product.CreateProduct(product);
            _manager.Save();
        }

        public void DeleteProduct(Product product)
        {
            _manager.Product.DeleteProduct(product);
            _manager.Save();
        }
    }
}