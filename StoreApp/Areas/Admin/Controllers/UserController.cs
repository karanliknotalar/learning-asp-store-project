using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers;

[Area("Admin")]
public class UserController : Controller
{
    private readonly IServiceManager _manager;

    public UserController(IServiceManager manager)
    {
        _manager = manager;
    }

    public IActionResult Index()
    {
        return View(_manager.AuthService.Users);
    }

    public IActionResult Create()
    {
        return View(new UserDtoForInsertion()
        {
            Roles = new HashSet<string>(_manager.AuthService.Roles.Select(r => r.Name).ToList())
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] UserDtoForInsertion userDtoForInsertion)
    {
        var result = await _manager.AuthService.CreateUser(userDtoForInsertion);

        if (result.Errors.Any())
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        return result.Succeeded
            ? RedirectToAction("Index")
            : View();
    }

    public async Task<IActionResult> Update([FromRoute(Name = "id")] string userName)
    {
        var userDto = await _manager.AuthService.GetOneUserForUpdate(userName);
        return View(userDto);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Update([FromForm] UserDtoForUpdate userDto)
    {
        if (ModelState.IsValid)
        {
            var result = await _manager.AuthService.UpdateUser(userDto);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
        }

        return RedirectToAction("Update");
    }

    public async Task<IActionResult> Delete([FromRoute(Name = "id")] string userName)
    {
        var result = await _manager.AuthService.DeleteUser(userName);
        if (result.Errors.Any())
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        return RedirectToAction("Index");
    }
}