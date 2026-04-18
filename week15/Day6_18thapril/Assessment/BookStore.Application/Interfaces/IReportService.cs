using BookStore.Application.DTOs;

namespace BookStore.Application.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<OrderResponseDto>> GetOrderReportAsync(DateTime from, DateTime to);
        Task<IEnumerable<BookDto>> GetLowStockBooksAsync(int threshold);
        Task<decimal> GetTotalRevenueAsync();
    }
}
