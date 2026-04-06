using LearningAPI.Data;
using LearningAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace LearningAPI.Controllers
{
    [ApiController]
    [Route("api/v1/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;

        public CoursesController(AppDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            if (!_cache.TryGetValue("courses", out List<Course>? courses))
            {
                courses = await _context.Courses.Include(c => c.Lessons).ToListAsync();
                _cache.Set("courses", courses, TimeSpan.FromMinutes(5));
            }
            return Ok(courses);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _context.Courses.Include(c => c.Lessons).FirstOrDefaultAsync(c => c.Id == id);
            if (course == null) throw new KeyNotFoundException("Course not found");
            return Ok(course);
        }

        [HttpPost]
        [Authorize(Roles = "Instructor,Admin")]
        public async Task<IActionResult> Create([FromBody] Course course)
        {
            if (string.IsNullOrWhiteSpace(course.Title))
                return BadRequest(new { error = "Title is required" });
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            _cache.Remove("courses");
            return Ok(course);
        }

        [HttpPost("{id}/lessons")]
        [Authorize(Roles = "Instructor,Admin")]
        public async Task<IActionResult> AddLesson(int id, [FromBody] LessonRequest request)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) throw new KeyNotFoundException("Course not found");

            var lesson = new Lesson { Name = request.Name, CourseId = id, Course = course };
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
            _cache.Remove("courses");
            return Ok(lesson);
        }

        [HttpPost("/api/v1/enroll")]
        [Authorize(Roles = "Student,Admin")]
        public async Task<IActionResult> Enroll([FromBody] EnrollRequest request)
        {
            var exists = await _context.Enrollments
                .AnyAsync(e => e.UserId == request.UserId && e.CourseId == request.CourseId);
            if (exists) return BadRequest(new { error = "Already enrolled" });

            var user = await _context.Users.FindAsync(request.UserId);
            var course = await _context.Courses.FindAsync(request.CourseId);
            if (user == null || course == null)
                return BadRequest(new { error = "User or Course not found" });

            var enrollment = new Enrollment { UserId = request.UserId, User = user, CourseId = request.CourseId, Course = course };
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Enrolled successfully!" });
        }

        [HttpGet("/api/v1/enrollments/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetEnrollments(int userId)
        {
            var enrollments = await _context.Enrollments
                .Where(e => e.UserId == userId)
                .ToListAsync();
            return Ok(enrollments);
        }
    }

    public class EnrollRequest
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
    }

    public class LessonRequest
    {
        public string Name { get; set; } = string.Empty;
    }
}
