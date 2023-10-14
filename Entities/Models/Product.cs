namespace Entities.Models;

public class Product
{
    public int ProductId { get; set; }
    public int? CategoryId { get; set; } // Foreign Key
    public string? ProductName { get; set; } = string.Empty;
    public string? Summary { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool ShowCase { get; set; }  
    public Category? Category { get; set; } // Navigation Property
}