using BookStore.Application.DTOs;

namespace BookStore.Web.Services.Interfaces
{
    public interface IBookApiService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto?> GetBookByIdAsync(int id);
        Task<IEnumerable<BookDto>> GetBooksByCategoryAsync(int categoryId);
        Task<IEnumerable<BookDto>> SearchBooksAsync(string keyword);
        Task<BookDto> CreateBookAsync(BookCreateDto dto);
        Task<BookDto> UpdateBookAsync(BookUpdateDto dto);
        Task DeleteBookAsync(int id);
    }
}
