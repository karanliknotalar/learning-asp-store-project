using Entities.Dtos;
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
        ViewBag.Categories = SelectListForCategory();
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] ProductDtoForInsertion productDto, [FromForm] IFormFile file)
    {
        if (ModelState.IsValid)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            productDto.ImageUrl = string.Concat("/images/", file.FileName);

            _manager.ProductServices.CreateProduct(productDto);
            return RedirectToAction("Index");
        }

        ViewBag.Categories = SelectListForCategory();
        return View();
    }

    public IActionResult Update([FromRoute(Name = "id")] int id)
    {
        var product = _manager.ProductServices.GetOneProductUpdate(id, false);
        if (product == null) throw new Exception("Product not found!");
        ViewBag.Categories = SelectListForCategory((int)product.CategoryId!);
        return View(product);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Update([FromForm] ProductDtoForInsertion productDto, [FromForm] IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            if (file?.FileName != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);
                await using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
            }

            productDto.ImageUrl = (file?.FileName != null)
                ? string.Concat("/images/", file.FileName)
                : GetProductImage(productDto.ProductId);

            _manager.ProductServices.UpdateProduct(productDto);
            return RedirectToAction("Index");
        }
        var product = _manager.ProductServices.GetOneProductUpdate(productDto.ProductId, false);
        if (product == null) throw new Exception("Product not found!");
        ViewBag.Categories = SelectListForCategory((int)productDto.CategoryId!);
        return View(product);
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

    private SelectList SelectListForCategory(int selectedCategoryId = 1)
    {
        return new SelectList(
            _manager.CategoryServices.GetAllCategories(false),
            "CategoryId",
            "CategoryName",
            selectedCategoryId
        );
    }

    private string GetProductImage(int id)
    {
        return _manager.ProductServices.GetOneProduct(id, false)?.ImageUrl ?? "";
    }
}