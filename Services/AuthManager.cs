using AutoMapper;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Services.Contracts;

namespace Services;

public class AuthManager : IAuthService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IMapper _mapper;

    public AuthManager(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _mapper = mapper;
    }

    public IEnumerable<IdentityRole> Roles => _roleManager.Roles;
    public IEnumerable<IdentityUser> Users => _userManager.Users;

    public async Task<IdentityUser> GetOneUser(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    public async Task<UserDtoForUpdate> GetOneUserForUpdate(string userName)
    {
        var user = await GetOneUser(userName);

        if (user is null)
            throw new Exception($"{userName} user not found.");

        var userDto = _mapper.Map<UserDtoForUpdate>(user);
        userDto.Roles = new HashSet<string>(Roles.Select(r => r.Name).ToList());
        userDto.UserRoles = new HashSet<string>(await _userManager.GetRolesAsync(user));
        userDto.CurrentUserName = user.UserName;

        return userDto;
    }

    public async Task<IdentityResult> CreateUser(UserDtoForInsertion userDto)
    {
        var user = _mapper.Map<IdentityUser>(userDto);
        var result = await _userManager.CreateAsync(user, userDto.Password);

        if (!result.Succeeded)
            throw new Exception("User could not be created.");

        if (userDto.Roles.Any())
        {
            var roleResul = await _userManager.AddToRolesAsync(user, userDto.Roles);
            if (!roleResul.Succeeded)
                throw new Exception("System have problems with roles");
        }

        return result;
    }

    public async Task<IdentityResult> UpdateUser(UserDtoForUpdate userDto)
    {
        var user = await GetOneUser(userDto.CurrentUserName!);
        if (user is null)
            throw new Exception($"{userDto.CurrentUserName} user not found.");

        user.UserName = userDto.UserName;
        user.Email = userDto.Email;
        user.PhoneNumber = userDto.PhoneNumber;

        var updateResult = await _userManager.UpdateAsync(user);

        if (!updateResult.Succeeded)
            throw new Exception($"{userDto.CurrentUserName} could not be updated.");

        if (userDto.Roles.Any())
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var removeRoleResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
            if (!removeRoleResult.Succeeded)
                throw new Exception($"{userDto.CurrentUserName} could not be removed roles for update");
            var updateRoleResult = await _userManager.AddToRolesAsync(user, userDto.Roles);
            if (!updateRoleResult.Succeeded)
                throw new Exception($"{userDto.CurrentUserName} could not be inserted roles for update");
        }

        return updateResult;
    }

    public async Task<IdentityResult> DeleteUser(string userName)
    {
        var user = await GetOneUser(userName);
        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
            throw new Exception("User could not be deleted.");
        return result;
    }

    public async Task<IdentityResult> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var user = await GetOneUser(resetPasswordDto.UserName!);
        if (user is null)
            throw new Exception($"{resetPasswordDto.UserName} user not found.");

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        if (token is null)
            throw new Exception($"{resetPasswordDto.UserName} user isin password reset token could not be created");

        return await _userManager.ResetPasswordAsync(user, token, resetPasswordDto.Password);
    }
}