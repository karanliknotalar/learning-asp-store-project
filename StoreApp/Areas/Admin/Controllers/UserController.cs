using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using StoreApp.Infrastructure.Extensions;

namespace StoreApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly IServiceManager _manager;

    public UserController(IServiceManager manager)
    {
        _manager = manager;
    }

    public  IActionResult Index()
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
        var result = await _manager.AuthService.CreateUserAsync(userDtoForInsertion);

        this.AddModelStateError(result.Errors);

        if (result.Succeeded)
        {
            TempData["success"] = "The user has been created successfully.";
            return RedirectToAction("Index");
        }

        return View();
    }

    public async Task<IActionResult> Update([FromRoute(Name = "id")] string userName)
    {
        var userDto = await _manager.AuthService.GetUserForUpdateAsync(userName);
        return View(userDto);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Update([FromForm] UserDtoForUpdate userDto)
    {
        if (ModelState.IsValid)
        {
            var result = await _manager.AuthService.UpdateUserAsync(userDto);

            this.AddModelStateError(result.Errors);

            if (result.Succeeded)
            {
                TempData["success"] = "The user has been updated successfully.";
                return RedirectToAction("Index");
            }
        }

        return View(await _manager.AuthService.GetUserForUpdateAsync(userDto.CurrentUserName!));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete([FromForm] UserDto userDto)
    {
        var result = await _manager.AuthService.DeleteUserAsync(userDto.UserName!);

        this.AddModelStateError(result.Errors);
        if (result.Succeeded)
            TempData["success"] = "The user has been updated successfully.";

        return RedirectToAction("Index");
    }

    public IActionResult ResetPassword([FromRoute(Name = "id")] string userName)
    {
        return View(new ResetPasswordDto()
        {
            UserName = userName
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDto model)
    {
        if (ModelState.IsValid)
        {
            var result = await _manager.AuthService.ResetPasswordAsync(model);

            this.AddModelStateError(result.Errors);

            if (result.Succeeded)
            {
                TempData["success"] = "The user has been updated successfully.";
                return RedirectToAction("Index");
            }
        }

        return View(new ResetPasswordDto()
        {
            UserName = model.UserName
        });
    }
}