using Microsoft.AspNetCore.Mvc;
using LibraryManagementMVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementMVC.Controllers
{
    public class BooksController : Controller
    {
        private static List<BookViewModel> books = new List<BookViewModel>()
        {
            new BookViewModel
            {
                Book = new Book { Id = 1, Title = "C# Basics", Author = "John", PublishedYear = 2020, Genre = "Programming" },
                IsAvailable = true,
                BorrowerName = ""
            }
        };

        public IActionResult Index()
        {
            ViewBag.Message = "Welcome to Library!";
            ViewData["TotalBooks"] = books.Count;

            return View(books);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Book.Id = books.Count + 1;
                books.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}