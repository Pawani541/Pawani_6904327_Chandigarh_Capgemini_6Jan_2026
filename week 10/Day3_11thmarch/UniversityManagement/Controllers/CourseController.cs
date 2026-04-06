using Microsoft.AspNetCore.Mvc;
using UniversityManagement.Models;
using System.Linq;

namespace UniversityManagement.Controllers
{
    public class CourseController : Controller
    {
        private readonly UniversityContext _context;

        public CourseController(UniversityContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Courses.ToList());
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var course = _context.Courses.FirstOrDefault(c => c.CourseId == id);

            if (course == null) return NotFound();

            return View(course);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(course);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var course = _context.Courses.Find(id);

            if (course == null) return NotFound();

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Course course)
        {
            if (id != course.CourseId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(course);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(course);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var course = _context.Courses.FirstOrDefault(c => c.CourseId == id);

            if (course == null) return NotFound();

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var course = _context.Courses.Find(id);

            if (course != null)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}