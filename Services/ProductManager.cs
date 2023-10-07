using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ProductManager : IProductService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public ProductManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public IEnumerable<Product> GetAllProduct(bool trackChanges)
        {
            return _manager.Product.GetAllProducts(trackChanges);
        }

        public Product GetOneProduct(int id, bool trackChanges)
        {
            return _manager.Product.GetOneProduct(id, trackChanges) ?? throw new Exception("Product not found!");
        }

        public void CreateProduct(ProductDtoForInsertion productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _manager.Product.CreateProduct(product);
            _manager.Save();
        }

        public void DeleteProduct(Product product)
        {
            _manager.Product.DeleteProduct(product);
            _manager.Save();
        }

        public void UpdateProduct(Product product)
        {
            _manager.Product.UpdateProduct(product);
            _manager.Save();

            // var model = _manager.Product.GetOneProduct(product.ProductId, true);
            // if (model != null)
            // {
            //     model.Price = product.Price;
            //     model.ProductName = product.ProductName;
            // }
            // _manager.Save();
        }
    }
}