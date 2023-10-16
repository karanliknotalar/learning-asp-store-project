using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;

namespace Services.Contracts
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProduct(bool trackChanges = false);
        IEnumerable<Product> GetAllProductsWithDetails(ProductRequestParameters? parameters);
        IEnumerable<Product> GetShowCaseProduct(bool trackChanges = false);
        Product? GetOneProduct(int id, bool trackChanges = false);
        void CreateProduct(ProductDtoForInsertion productDto);
        void DeleteProduct(Product product);
        void UpdateProduct(ProductDtoForInsertion productDto);
        ProductDtoForUpdate? GetOneProductUpdate(int id, bool trackChanges = false);
    }
}
