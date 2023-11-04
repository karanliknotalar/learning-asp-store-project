using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Components;

public class ShowcaseViewComponent : ViewComponent
{
    private readonly IServiceManager _manager;

    public ShowcaseViewComponent(IServiceManager manager)
    {
        _manager = manager;
    }

    public IViewComponentResult Invoke(string views = "Default")
    {
        var products = _manager.ProductServices.GetShowCaseProduct();
        return views.Equals("Default")
            ? View(products)
            : View("List", products);
    }
}