using Entities.Models;

namespace Services.Contracts;

public interface IOrderService
{
    IEnumerable<Order> Orders { get;  }
    Order? GetOneOrder(int id);
    void Complete (int id);
    void SaveOrder(Order order);
    int NumberOfInProgress { get; }
}