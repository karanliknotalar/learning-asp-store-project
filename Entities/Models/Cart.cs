namespace Entities.Models;

public class Cart
{
    public List<CartLine> Lines { get; set; } = new();

    public void AddItem(Product product, int quantity)
    {
        CartLine? line = Lines.FirstOrDefault(l => l.Product.ProductId.Equals(product.ProductId));
        if (line is null)
        {
            Lines.Add(new CartLine()
            {
                Product = product,
                Quantity = quantity
            });
        }
        else
        {
            line.Quantity += quantity;
        }
    }

    public void RemoveLine(Product product)
    {
        Lines.RemoveAll(l => l.Product.ProductId.Equals(product.ProductId));
    }

    public decimal ComputeTotalValue() => Lines.Sum(i => i.Product.Price * i.Quantity);

    public void Clear() => Lines.Clear();
}