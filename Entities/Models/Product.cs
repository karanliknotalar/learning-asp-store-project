using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Entities.Models;

public class Product
{
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Product name is not empty.")]
    public string? ProductName { get; set; } = string.Empty;

    [Required(ErrorMessage = "price name is not empty.")]
    [Range(0, int.MaxValue)]
    public decimal Price { get; set; } = 0;
}