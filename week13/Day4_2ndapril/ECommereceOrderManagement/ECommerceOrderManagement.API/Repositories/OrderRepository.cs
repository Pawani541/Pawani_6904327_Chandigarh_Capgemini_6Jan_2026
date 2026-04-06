using Microsoft.EntityFrameworkCore;
using ECommerceOrderManagement.API.Data;
using ECommerceOrderManagement.API.Models;
using ECommerceOrderManagement.API.Repositories.Interfaces;

namespace ECommerceOrderManagement.API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _ctx;

        public OrderRepository(AppDbContext ctx) { _ctx = ctx; }

        public async Task<List<Order>> GetAllAsync() =>
            await _ctx.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

        public async Task<Order?> GetByIdAsync(int id) =>
            await _ctx.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

        public async Task<List<Order>> GetByUserIdAsync(int userId) =>
            await _ctx.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId)
                .ToListAsync();

        public async Task AddAsync(Order order)
        {
            await _ctx.Orders.AddAsync(order);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _ctx.Orders.Update(order);
            await _ctx.SaveChangesAsync();
        }
    }
}
