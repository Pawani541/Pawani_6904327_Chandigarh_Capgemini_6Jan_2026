using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model_level_Validation.Models;

public class CompaniesController : Controller
{
    private readonly AppDbContext _context;

    public CompaniesController(AppDbContext context)
    {
        _context = context;
    }

    // READ
    public IActionResult Index()
    {
        var data = _context.Companies.Include(c => c.Employees).ToList();
        return View(data);
    }

    // CREATE
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Company company)
    {
        if (ModelState.IsValid)
        {
            _context.Companies.Add(company);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(company);
    }

    // EDIT
    public IActionResult Edit(int id)
    {
        var company = _context.Companies.Find(id);
        return View(company);
    }

    [HttpPost]
    public IActionResult Edit(Company company)
    {
        _context.Companies.Update(company);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    // DELETE
    public IActionResult Delete(int id)
    {
        var company = _context.Companies.Find(id);
        _context.Companies.Remove(company);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}