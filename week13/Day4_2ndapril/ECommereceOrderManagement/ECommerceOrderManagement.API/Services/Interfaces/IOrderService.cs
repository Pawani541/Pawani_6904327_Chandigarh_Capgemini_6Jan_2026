using ECommerceOrderManagement.API.DTOs;

namespace ECommerceOrderManagement.API.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderResponseDto>> GetAllOrdersAsync();
        Task<OrderResponseDto?> GetOrderByIdAsync(int id);
        Task<List<OrderResponseDto>> GetOrdersByUserAsync(int userId);
        Task<OrderResponseDto> PlaceOrderAsync(PlaceOrderDto dto);
        Task<bool> UpdateOrderStatusAsync(int orderId, string status);
    }
}
