using OrderAPI.Api.Models;

namespace OrderAPI.Api.Services
{
    public interface IOrderService
    {
        Task<bool> PlaceOrderAsync(Order order);
    }
}
