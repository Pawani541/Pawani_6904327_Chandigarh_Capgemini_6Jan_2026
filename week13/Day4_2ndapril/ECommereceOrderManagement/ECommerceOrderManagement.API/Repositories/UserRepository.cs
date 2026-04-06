using Microsoft.EntityFrameworkCore;
using ECommerceOrderManagement.API.Data;
using ECommerceOrderManagement.API.Models;
using ECommerceOrderManagement.API.Repositories.Interfaces;

namespace ECommerceOrderManagement.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _ctx;

        public UserRepository(AppDbContext ctx) { _ctx = ctx; }

        public async Task<User?> GetByEmailAsync(string email) =>
            await _ctx.Users
                .Include(u => u.RefreshToken)
                .FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User?> GetByIdAsync(int id) =>
            await _ctx.Users
                .Include(u => u.RefreshToken)
                .FirstOrDefaultAsync(u => u.Id == id);

        public async Task AddAsync(User user)
        {
            await _ctx.Users.AddAsync(user);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _ctx.Users.Update(user);
            await _ctx.SaveChangesAsync();
        }
    }
}
