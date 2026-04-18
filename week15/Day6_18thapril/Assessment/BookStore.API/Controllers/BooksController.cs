using Asp.Versioning;
using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IValidator<BookCreateDto> _createValidator;
        private readonly IValidator<BookUpdateDto> _updateValidator;

        public BooksController(
            IBookService bookService,
            IValidator<BookCreateDto> createValidator,
            IValidator<BookUpdateDto> updateValidator)
        {
            _bookService = bookService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound(new { message = $"Book with ID {id} not found." });
            return Ok(book);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var books = await _bookService.GetBooksByCategoryAsync(categoryId);
            return Ok(books);
        }

        [HttpGet("instock")]
        public async Task<IActionResult> GetInStock()
        {
            var books = await _bookService.GetBooksInStockAsync();
            return Ok(books);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest(new { message = "Search keyword cannot be empty." });

            var books = await _bookService.SearchBooksAsync(keyword);
            return Ok(books);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] BookCreateDto dto)
        {
            var validation = await _createValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(e => e.ErrorMessage));

            var book = await _bookService.CreateBookAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = book.BookId }, book);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] BookUpdateDto dto)
        {
            if (id != dto.BookId)
                return BadRequest(new { message = "ID mismatch." });

            var validation = await _updateValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(e => e.ErrorMessage));

            var book = await _bookService.UpdateBookAsync(dto);
            return Ok(book);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
