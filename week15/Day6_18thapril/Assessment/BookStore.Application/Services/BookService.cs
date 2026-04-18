using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;

namespace BookStore.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return book == null ? null : _mapper.Map<BookDto>(book);
        }

        public async Task<IEnumerable<BookDto>> GetBooksByCategoryAsync(int categoryId)
        {
            var books = await _bookRepository.GetBooksByCategoryAsync(categoryId);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<IEnumerable<BookDto>> GetBooksInStockAsync()
        {
            var books = await _bookRepository.GetBooksInStockAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<IEnumerable<BookDto>> SearchBooksAsync(string keyword)
        {
            var books = await _bookRepository.SearchBooksAsync(keyword);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> CreateBookAsync(BookCreateDto dto)
        {
            var book = _mapper.Map<Book>(dto);
            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveAsync();
            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> UpdateBookAsync(BookUpdateDto dto)
        {
            var book = await _bookRepository.GetByIdAsync(dto.BookId);
            if (book == null)
                throw new KeyNotFoundException($"Book with ID {dto.BookId} not found.");

            _mapper.Map(dto, book);
            await _bookRepository.UpdateAsync(book);
            await _bookRepository.SaveAsync();
            return _mapper.Map<BookDto>(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                throw new KeyNotFoundException($"Book with ID {id} not found.");

            await _bookRepository.DeleteAsync(id);
            await _bookRepository.SaveAsync();
        }
    }
}
