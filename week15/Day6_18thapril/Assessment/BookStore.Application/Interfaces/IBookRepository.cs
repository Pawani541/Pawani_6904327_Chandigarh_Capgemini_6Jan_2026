using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId);
        Task<IEnumerable<Book>> GetBooksInStockAsync();
        Task<IEnumerable<Book>> SearchBooksAsync(string keyword);
    }
}
