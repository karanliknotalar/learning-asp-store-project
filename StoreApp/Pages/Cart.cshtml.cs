using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Contracts;

namespace StoreApp.Pages;

public class CartModel : PageModel
{
    public Cart Cart { get; set; }
    public string ReturnUrl { get; set; } = "/";

    private readonly IServiceManager _manager;


    public CartModel(IServiceManager manager, Cart cart)
    {
        _manager = manager;
        Cart = cart;
    }

    public void OnGet(string? returnUrl)
    {
        ReturnUrl = returnUrl ?? "/";
    }

    public IActionResult OnPost(int productId, string returnUrl)
    {
        var product = _manager.ProductServices.GetOneProduct(productId);

        if (product is not null)
        {
            Cart.AddItem(product, 1);
        }

        return RedirectToPage(new { returnUrl = returnUrl});
    }

    public IActionResult OnPostRemove(int productId, string returnUrl)
    {
        var product = Cart.Lines.FirstOrDefault(p => p.Product.ProductId.Equals(productId))?.Product;
        if (product is not null)
        {
            Cart.RemoveLine(product);
        }
        return Page();
    }
}