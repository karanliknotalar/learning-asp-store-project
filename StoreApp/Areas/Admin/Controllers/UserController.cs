using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using StoreApp.Infrastructure.Extensions;

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

        this.AddModelStateError(result.Errors);

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
            
            this.AddModelStateError(result.Errors);
            
            if (result.Succeeded)
                return RedirectToAction("Index");
            
        }

        return View(await _manager.AuthService.GetOneUserForUpdate(userDto.CurrentUserName!));
    }

    public async Task<IActionResult> Delete([FromRoute(Name = "id")] string userName)
    {
        var result = await _manager.AuthService.DeleteUser(userName);

        this.AddModelStateError(result.Errors);

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
            var result = await _manager.AuthService.ResetPassword(model);

            this.AddModelStateError(result.Errors);

            if (result.Succeeded)
                return RedirectToAction("Index");
        }

        return View(new ResetPasswordDto()
        {
            UserName = model.UserName
        });
    }
}