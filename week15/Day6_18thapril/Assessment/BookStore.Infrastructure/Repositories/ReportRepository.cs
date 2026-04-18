using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ReportRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderResponseDto>> GetOrderReportAsync(DateTime from, DateTime to)
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .Where(o => o.OrderDate >= from && o.OrderDate <= to)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
        }

        public async Task<IEnumerable<BookDto>> GetLowStockBooksAsync(int threshold)
        {
            var books = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Where(b => b.Stock <= threshold)
                .OrderBy(b => b.Stock)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _context.Orders
                .Where(o => o.Status != "Cancelled")
                .SumAsync(o => o.TotalAmount);
        }
    }
}
