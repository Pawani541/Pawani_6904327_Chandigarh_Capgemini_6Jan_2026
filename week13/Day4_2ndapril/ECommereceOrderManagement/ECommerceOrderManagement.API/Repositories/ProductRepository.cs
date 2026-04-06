using Microsoft.EntityFrameworkCore;
using ECommerceOrderManagement.API.Data;
using ECommerceOrderManagement.API.Models;
using ECommerceOrderManagement.API.Repositories.Interfaces;

namespace ECommerceOrderManagement.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _ctx;

        public ProductRepository(AppDbContext ctx) { _ctx = ctx; }

        public async Task<List<Product>> GetAllAsync() =>
            await _ctx.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .ToListAsync();

        public async Task<Product?> GetByIdAsync(int id) =>
            await _ctx.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task AddAsync(Product product)
        {
            await _ctx.Products.AddAsync(product);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _ctx.Products.Update(product);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _ctx.Products.Remove(product);
            await _ctx.SaveChangesAsync();
        }
    }
}
