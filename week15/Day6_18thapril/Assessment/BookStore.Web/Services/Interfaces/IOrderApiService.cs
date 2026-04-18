using BookStore.Application.DTOs;

namespace BookStore.Web.Services.Interfaces
{
    public interface IOrderApiService
    {
        Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync();
        Task<OrderResponseDto?> GetOrderByIdAsync(int id);
        Task<IEnumerable<OrderResponseDto>> GetOrdersByUserAsync(int userId);
        Task<OrderResponseDto> PlaceOrderAsync(OrderCreateDto dto);
    }
}
