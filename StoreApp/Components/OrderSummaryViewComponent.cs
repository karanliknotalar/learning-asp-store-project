using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Components;

public class OrderSummaryViewComponent : ViewComponent
{
    private readonly IServiceManager _manager;

    public OrderSummaryViewComponent(IServiceManager manager)
    {
        _manager = manager;
    }

    public string Invoke()
    {
        return _manager.OrderService.NumberOfInProgress.ToString();
    }
}