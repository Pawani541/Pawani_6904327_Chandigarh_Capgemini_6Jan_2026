using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId)
        {
            return await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Where(b => b.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksInStockAsync()
        {
            return await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Where(b => b.Stock > 0)
                .OrderByDescending(b => b.Price)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string keyword)
        {
            return await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Where(b => b.Title.Contains(keyword) || b.ISBN.Contains(keyword))
                .ToListAsync();
        }
    }
}
