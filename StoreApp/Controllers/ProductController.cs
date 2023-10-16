using Entities.RequestParameters;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index(ProductRequestParameters parameters)
        {
            var products = _manager.ProductServices.GetAllProductsWithDetails(parameters);
            return View(products);
        }

        public IActionResult Get([FromRoute(Name = "id")] int id)
        {
            var product = _manager.ProductServices.GetOneProduct(id);
            return View(product);
        }
    }
}