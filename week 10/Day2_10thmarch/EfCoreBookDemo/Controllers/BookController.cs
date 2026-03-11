using Microsoft.AspNetCore.Mvc;
using System.Linq;
using EfCoreBookDemo.Models;

public class BookController : Controller
{
    private readonly BookDBContext _context;

    public BookController(BookDBContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var books = _context.books.ToList();
        return View(books);
    }

    // GET: Create page open karega
    public IActionResult Create()
    {
        return View();
    }

    // POST: Book database me save karega
    [HttpPost]
    public IActionResult Create(BookModel book)
    {
        _context.books.Add(book);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}