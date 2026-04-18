using BookStore.Application.DTOs;
using BookStore.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookApiService _bookService;

        public BooksController(IBookApiService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index(string? search)
        {
            IEnumerable<BookDto> books;
            if (!string.IsNullOrWhiteSpace(search))
                books = await _bookService.SearchBooksAsync(search);
            else
                books = await _bookService.GetAllBooksAsync();

            ViewBag.Search = search;
            return View(books);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookCreateDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _bookService.CreateBookAsync(dto);
            TempData["Success"] = "Book created successfully!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToAction("Login", "Account");

            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();

            var dto = new BookUpdateDto
            {
                BookId = book.BookId,
                Title = book.Title,
                ISBN = book.ISBN,
                Price = book.Price,
                Stock = book.Stock,
                ImageUrl = book.ImageUrl
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookUpdateDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _bookService.UpdateBookAsync(dto);
            TempData["Success"] = "Book updated successfully!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToAction("Login", "Account");

            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookService.DeleteBookAsync(id);
            TempData["Success"] = "Book deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
