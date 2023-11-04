using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
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

        public IEnumerable<Product> GetAllProductsWithDetails(ProductRequestParameters? parameters)
        {
            return _manager.Product.GetAllProductsWithDetails(parameters);
        }

        public IEnumerable<Product>? GetAllProductsWithCategories(bool trackChanges = false)
        {
            return _manager.Product.GetAllProductsWithCategories(trackChanges);
        }

        public IEnumerable<Product> GetShowCaseProduct(bool trackChanges)
        {
            return _manager.Product.GetShowCaseProduct(trackChanges);
        }

        public IEnumerable<Product> GetLatestProduct(int n, bool trackChanges = false)
        {
            return _manager.Product
                .FindAll(trackChanges)
                .OrderByDescending(p => p.ProductId)
                .Take(n);
        }

        public Product GetOneProduct(int id, bool trackChanges)
        {
            return _manager.Product.GetOneProduct(id, trackChanges)
                   ?? throw new Exception("Product not found!");
        }

        public ProductDtoForUpdate? GetOneProductUpdate(int id, bool trackChanges)
        {
            var product = _manager.Product.GetOneProduct(id, trackChanges)
                          ?? throw new Exception("Product not found!");
            return _mapper.Map<ProductDtoForUpdate>(product);
        }

        public void SwitchProductShowCase(ProductDtoForUpdate update, bool trackChanges = false)
        {
            var product = _manager.Product.GetOneProduct(update.ProductId, trackChanges);
            product!.ShowCase = update.ShowCase;
            _manager.Product.UpdateProduct(product);
            _manager.Save();
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

        public void UpdateProduct(ProductDtoForUpdate productDto)
        {
            var product = _mapper.Map<Product>(productDto);
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