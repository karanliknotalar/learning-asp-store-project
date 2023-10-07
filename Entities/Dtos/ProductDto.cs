using System.ComponentModel.DataAnnotations;
using Entities.Models;

namespace Entities.Dtos;

public record ProductDto
{
    public int ProductId { get; init; }
    public int? CategoryId { get; init; }

    [Required(ErrorMessage = "Product name is not empty.")]
    public string? ProductName { get; init; } = string.Empty;

    [Required(ErrorMessage = "price name is not empty.")]
    [Range(0, int.MaxValue)]
    public decimal Price { get; init; } = 0;
}