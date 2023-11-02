﻿using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Controllers;

public class OrderController : Controller
{
    private readonly IServiceManager _manager;
    private readonly Cart _cart;

    public OrderController(IServiceManager manager, Cart cart)
    {
        _manager = manager;
        _cart = cart;
    }

    [Authorize]
    public ViewResult Checkout()
    {
        if (!_cart.Lines.Any())
        {
            ModelState.AddModelError("", "Sorry, your cart is empty!");
        }

        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Checkout([FromForm] Order order)
    {
        if (!ModelState.IsValid) return View();

        order.Lines = _cart.Lines.ToArray();
        _manager.OrderService.SaveOrder(order);
        _cart.Clear();
        return RedirectToPage("/Complete", new { OrderId = order.OrderId });
    }
}