using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers;

[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IServiceManager _manager;

    public ProductsController(IServiceManager manager)
    {
        _manager = manager;
    }

    [Route("api/products")]
    [HttpGet]
    public IActionResult GetAllProducts()
    {
        return Ok(_manager.ProductServices.GetAllProduct());
    }
}