using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Components;

public class UserSummaryViewComponent : ViewComponent
{
    private readonly IServiceManager _manager;

    public UserSummaryViewComponent(IServiceManager manager)
    {
        _manager = manager;
    }

    public string Invoke()
    {
        var count = _manager.AuthService.Users.Count().ToString();
        _manager.AuthService.Dispose();
        return count;
    }
}