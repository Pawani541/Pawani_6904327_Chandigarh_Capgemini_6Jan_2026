using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;

namespace BookStore.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<IEnumerable<OrderResponseDto>> GetOrderReportAsync(DateTime from, DateTime to)
        {
            return await _reportRepository.GetOrderReportAsync(from, to);
        }

        public async Task<IEnumerable<BookDto>> GetLowStockBooksAsync(int threshold)
        {
            return await _reportRepository.GetLowStockBooksAsync(threshold);
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _reportRepository.GetTotalRevenueAsync();
        }
    }
}
