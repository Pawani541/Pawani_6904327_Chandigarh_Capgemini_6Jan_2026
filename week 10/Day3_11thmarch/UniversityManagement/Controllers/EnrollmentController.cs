using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversityManagement.Models;

namespace UniversityManagement.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly UniversityContext _context;

        public EnrollmentController(UniversityContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var enrollments = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToList();

            return View(enrollments);
        }

        public IActionResult Create()
        {
            ViewBag.StudentId = new SelectList(_context.Students, "StudentId", "FullName");
            ViewBag.CourseId = new SelectList(_context.Courses, "CourseId", "Title");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Enrollment enrollment)
        {
            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}