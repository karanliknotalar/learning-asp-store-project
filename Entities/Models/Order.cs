using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Order
{
    public int OrderId { get; set; }
    public ICollection<CartLine> Lines { get; set; } = new List<CartLine>();
    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Adress1 is required")]
    public string? Adress1 { get; set; } 
    public string Adress2 { get; set; } = string.Empty;
    public string Adress3 { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public bool GiftWrap { get; set; }
    public bool Shipped { get; set; }
    
    public DateTime OrderedAt { get; set; } = DateTime.Now;
}