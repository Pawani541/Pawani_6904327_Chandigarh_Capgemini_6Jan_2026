using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Repositories;

namespace LibraryManagement.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _repo;

        public BooksController(IBookRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var books = _repo.GetAllBooks();
            return View(books);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            _repo.AddBook(book);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var book = _repo.GetBookById(id);
            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            _repo.UpdateBook(book);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _repo.DeleteBook(id);
            return RedirectToAction("Index");
        }
    }
}