namespace Entities.Dtos;

public record UserDtoForUpdate: UserDto
{
    public string? CurrentUserName { get; set; }
    public HashSet<string> UserRoles { get; set; } = new HashSet<string>();
}