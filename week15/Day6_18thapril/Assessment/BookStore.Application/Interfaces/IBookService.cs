using BookStore.Application.DTOs;

namespace BookStore.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto?> GetBookByIdAsync(int id);
        Task<IEnumerable<BookDto>> GetBooksByCategoryAsync(int categoryId);
        Task<IEnumerable<BookDto>> GetBooksInStockAsync();
        Task<IEnumerable<BookDto>> SearchBooksAsync(string keyword);
        Task<BookDto> CreateBookAsync(BookCreateDto dto);
        Task<BookDto> UpdateBookAsync(BookUpdateDto dto);
        Task DeleteBookAsync(int id);
    }
}
