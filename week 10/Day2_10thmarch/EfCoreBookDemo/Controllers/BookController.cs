using Microsoft.AspNetCore.Mvc;
using EfCoreBookDemo.Models;

namespace EfCoreBookDemo.Controllers
{
    public class BookController : Controller
    {
        private readonly BookDBContext _context;

        public BookController(BookDBContext context)
        {
            _context = context;
        }

        // LIST
        public IActionResult Index()
        {
            var books = _context.BookModels.ToList();
            return View(books);
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookModel book)
        {
            if (ModelState.IsValid)
            {
                _context.BookModels.Add(book);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        // EDIT (GET)
        public IActionResult Edit(int id)
        {
            var book = _context.BookModels.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookModel book)
        {
            if (ModelState.IsValid)
            {
                _context.BookModels.Update(book);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            var book = _context.BookModels.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // DELETE (GET)
        public IActionResult Delete(int id)
        {
            var book = _context.BookModels.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // DELETE (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _context.BookModels.Find(id);

            if (book != null)
            {
                _context.BookModels.Remove(book);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }


}
