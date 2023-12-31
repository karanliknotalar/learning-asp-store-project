using Entities.RequestParameters;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using StoreApp.Models;

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
            var pagination = new Pagination
            {
                CurrentPage = parameters.PageNumber,
                ItemsPerPage = parameters.PageSize,
                TotalItems = _manager.ProductServices.GetAllProduct().Count()
            };
            ViewData["title"] = "Products";
            return View(new ProductListViewModel
            {
                Products = products,
                Pagination = pagination
            });
        }

        public IActionResult Get([FromRoute(Name = "id")] int id)
        {
            var product = _manager.ProductServices.GetOneProduct(id);
            ViewData["title"] = product?.ProductName;
            return View(product);
        }
    }
}