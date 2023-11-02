using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos;

public record ResetPasswordDto
{
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Username is required")]
    public string? UserName { get; init; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; init; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "ConfirmPassword is required")]
    [Compare("Password", ErrorMessage = "Passwords must be match.")]
    public string? ConfirmPassword { get; init; }
}