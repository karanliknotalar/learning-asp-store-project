using System.Linq.Expressions;
using Entities.Models;

namespace Repositories.Contracts;

public interface IOrderRepository: IRepositoryBase<Order>
{
    IEnumerable<Order> Orders { get;  }
    Order? GetOneOrder(int id);
    void Complete (int id);
    void SaveOrder(Order order);
    int NumberOfInProgress { get; }
}