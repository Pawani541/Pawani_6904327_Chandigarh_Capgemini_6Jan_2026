using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model_level_Validation.Models;

namespace Model_level_Validation.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 CREATE (GET)
        public IActionResult Create()
        {
            ViewBag.Companies = new SelectList(_context.Companies, "CompanyId", "Name");
            return View();
        }

        // 🔹 CREATE (POST)
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();

                // 🔥 IMPORTANT: Redirect to Companies Index
                return RedirectToAction("Index", "Companies");
            }

            // 🔁 reload dropdown if validation fails
            ViewBag.Companies = new SelectList(_context.Companies, "CompanyId", "Name");
            return View(employee);
        }

        // 🔹 EDIT (GET)
        public IActionResult Edit(int id)
        {
            var employee = _context.Employees.Find(id);

            ViewBag.Companies = new SelectList(_context.Companies, "CompanyId", "Name", employee.CompanyId);
            return View(employee);
        }

        // 🔹 EDIT (POST)
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Update(employee);
                _context.SaveChanges();

                return RedirectToAction("Index", "Companies");
            }

            ViewBag.Companies = new SelectList(_context.Companies, "CompanyId", "Name", employee.CompanyId);
            return View(employee);
        }

        // 🔹 DELETE
        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Companies");
        }
    }
}