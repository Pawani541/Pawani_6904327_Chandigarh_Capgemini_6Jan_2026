using BookStore.Application.DTOs;

namespace BookStore.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync();
        Task<OrderResponseDto?> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<OrderResponseDto>> GetOrdersByUserAsync(int userId);
        Task<OrderResponseDto> PlaceOrderAsync(OrderCreateDto dto);
        Task UpdateOrderStatusAsync(int orderId, string status);
    }
}
