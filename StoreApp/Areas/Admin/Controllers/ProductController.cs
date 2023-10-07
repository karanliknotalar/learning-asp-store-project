using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        // ViewBag.Categories = _manager.CategoryServices.GetAllCategories(false);
        var categories = _manager.CategoryServices.GetAllCategories(false);
        ViewBag.Categories = new SelectList(
            categories,
            "CategoryId",
            "CategoryName",
            "1"
        );
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create([FromForm] ProductDtoForInsertion productDto)
    {
        if (!ModelState.IsValid) return View();

        _manager.ProductServices.CreateProduct(productDto);
        return RedirectToAction("Index");
    }

    public IActionResult Update([FromRoute(Name = "id")] int id)
    {
        var product = _manager.ProductServices.GetOneProduct(id, false);
        // ViewBag.Categories = _manager.CategoryServices.GetAllCategories(false);
        var categories = _manager.CategoryServices.GetAllCategories(false);
        if (product == null) throw new Exception("Product not found!");
        ViewBag.Categories = new SelectList(
            categories,
            "CategoryId",
            "CategoryName",
            product.CategoryId
        );
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