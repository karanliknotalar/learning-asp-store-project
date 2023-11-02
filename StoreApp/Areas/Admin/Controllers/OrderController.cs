using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class OrderController : Controller
{
    private readonly IServiceManager _manager;

    public OrderController(IServiceManager manager)
    {
        _manager = manager;
    }

    // GET
    public IActionResult Index()
    {
        var orders = _manager.OrderService.Orders;
        return View(orders);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Complete([FromForm] int orderId)
    {
        _manager.OrderService.Complete(orderId);
        return RedirectToAction("Index");
    }
}