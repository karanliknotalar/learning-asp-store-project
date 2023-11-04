using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;
using StoreApp.Models;

namespace StoreApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ProductController : Controller
{
    private readonly IServiceManager _manager;

    public ProductController(IServiceManager manager)
    {
        _manager = manager;
    }

    public IActionResult Index([FromQuery] ProductRequestParameters parameters)
    {
        var products = _manager.ProductServices.GetAllProductsWithDetails(parameters);
        var pagination = new Pagination
        {
            CurrentPage = parameters.PageNumber,
            ItemsPerPage = parameters.PageSize,
            TotalItems = _manager.ProductServices.GetAllProduct().Count()
        };
        return View(new ProductListViewModel
        {
            Products = products,
            Pagination = pagination
        });
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
            TempData["success"] = "The product has been created successfully.";
            return RedirectToAction("Index");
        }

        ViewBag.Categories = SelectListForCategory();
        return View();
    }

    public IActionResult Update([FromRoute(Name = "id")] int id)
    {
        var product = _manager.ProductServices.GetOneProductUpdate(id);
        if (product is null) throw new Exception("Product not found!");
        ViewBag.Categories = SelectListForCategory((int)product.CategoryId!);
        return View(product);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Update([FromForm] ProductDtoForUpdate productDto, [FromForm] IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            if (file?.FileName is not null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);
                await using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
            }

            productDto.ImageUrl = (file?.FileName != null)
                ? string.Concat("/images/", file.FileName)
                : GetProductImage(productDto.ProductId);

            _manager.ProductServices.UpdateProduct(productDto);
            TempData["success"] = "The product has been updated successfully.";
            return RedirectToAction("Index");
        }

        var product = _manager.ProductServices.GetOneProductUpdate(productDto.ProductId);
        if (product is null) throw new Exception("Product not found!");
        ViewBag.Categories = SelectListForCategory((int)productDto.CategoryId!);
        return View(product);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult SwitchStatus([FromBody] ProductDtoForUpdate update)
    {
        _manager.ProductServices.SwitchProductShowCase(update);
        return Json(new { status = "ok" });
    }

    public IActionResult Delete([FromRoute(Name = "id")] int id)
    {
        var product = _manager.ProductServices.GetOneProduct(id);
        if (product is not null)
        {
            _manager.ProductServices.DeleteProduct(product);
            TempData["success"] = "The product has been deleted successfully.";
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
        return _manager.ProductServices.GetOneProduct(id)?.ImageUrl ?? "";
    }
}