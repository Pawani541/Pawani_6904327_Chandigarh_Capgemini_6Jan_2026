using OrderApi.Models;

namespace OrderApi.Repositories;

public interface IOrderRepository
{
    Task AddOrder(Order order);
}