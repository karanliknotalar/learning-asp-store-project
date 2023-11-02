using Entities.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts;

public interface IAuthService
{
    IEnumerable<IdentityRole> Roles { get; }
    IEnumerable<IdentityUser> Users { get; }
    Task<IdentityUser> GetUser(string userName);
    Task<UserDtoForUpdate> GetUserForUpdate(string userName);
    Task<IdentityResult> CreateUser(UserDtoForInsertion userDto);
    Task<IdentityResult> UpdateUser(UserDtoForUpdate userDto);
    Task<IdentityResult> DeleteUser(string userName);
    Task<IdentityResult> ResetPassword(ResetPasswordDto resetPasswordDto);
}