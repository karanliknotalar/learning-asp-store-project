using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IServiceManager _manager;

    public ProductController(IServiceManager manager)
    {
        _manager = manager;
    }

    public IActionResult Index()
    {
        var product = _manager.ProductServices.GetAllProduct(false);
        return View(product);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create([FromForm] Product product)
    {
        if (!ModelState.IsValid) return View();

        _manager.ProductServices.CreateProduct(product);
        return RedirectToAction("Index");
    }

    public IActionResult Update([FromRoute(Name = "id")] int id)
    {
        var product = _manager.ProductServices.GetOneProduct(id, false);
        return View(product);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Update(Product product)
    {
        if (!ModelState.IsValid) return View();

        _manager.ProductServices.UpdateProduct(product);
        return RedirectToAction("Index");
    }

    public IActionResult Delete([FromRoute(Name = "id")] int id)
    {
        var product = _manager.ProductServices.GetOneProduct(id, false);
        if (product != null)
        {
            _manager.ProductServices.DeleteProduct(product);
        }

        return RedirectToAction("Index");
    }
}