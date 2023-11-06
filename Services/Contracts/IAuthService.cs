using Entities.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts;

public interface IAuthService
{
    IEnumerable<IdentityRole> Roles { get; }
    IEnumerable<IdentityUser> Users { get; }
    Task<IdentityUser> GetUserAsync(string userName);
    Task<IList<string>> GetUserRolesAsync(string userName);
    Task<UserDtoForUpdate> GetUserForUpdateAsync(string userName);
    Task<IdentityResult> CreateUserAsync(UserDtoForInsertion userDto);
    Task<IdentityResult> UpdateUserAsync(UserDtoForUpdate userDto);
    Task<IdentityResult> DeleteUserAsync(string userName);
    Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
    Task<bool> UserIsInRoleAsync(string userName, string role);
    void Dispose();
}