using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Order>> GetOrdersByUserAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderWithItemsAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }
    }
}
