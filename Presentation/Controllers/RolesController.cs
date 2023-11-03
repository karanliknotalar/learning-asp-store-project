using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers;

[ApiController]
public class RolesController : ControllerBase
{
    private readonly IServiceManager _manager;

    public RolesController(IServiceManager manager)
    {
        _manager = manager;
    }

    [Route("/api/users")]
    [HttpGet]
    public IActionResult AllRoles()
    {
        return Ok(_manager.AuthService.Roles);
    }

    [Route("/api/users/{id}")]
    [HttpGet]
    public async Task<IActionResult> GetUserRoles(string id)
    {
        return Ok(await _manager.AuthService.GetUserRolesAsync(id));
    }
}