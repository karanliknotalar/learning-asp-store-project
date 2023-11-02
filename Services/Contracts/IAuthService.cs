using Entities.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts;

public interface IAuthService
{
    IEnumerable<IdentityRole> Roles { get; }
    IEnumerable<IdentityUser> Users { get; }
    Task<IdentityUser> GetOneUser(string userName);
    Task<UserDtoForUpdate> GetOneUserForUpdate(string userName);
    Task<IdentityResult> CreateUser(UserDtoForInsertion userDto);
    Task<IdentityResult> UpdateUser(UserDtoForUpdate userDto);
    Task<IdentityResult> DeleteUser(string userName);
    Task<IdentityResult> ResetPassword(ResetPasswordDto resetPasswordDto);
}