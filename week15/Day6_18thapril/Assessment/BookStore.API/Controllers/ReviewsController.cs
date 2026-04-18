using Asp.Versioning;
using BookStore.Application.DTOs;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReviewsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetByBook(int bookId)
        {
            var reviews = await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Book)
                .Where(r => r.BookId == bookId)
                .Select(r => new ReviewDto
                {
                    ReviewId = r.ReviewId,
                    BookId = r.BookId,
                    BookTitle = r.Book.Title,
                    UserId = r.UserId,
                    UserName = r.User.FullName,
                    Rating = r.Rating,
                    Comment = r.Comment
                })
                .ToListAsync();
            return Ok(reviews);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ReviewDto dto)
        {
            if (dto.Rating < 1 || dto.Rating > 5)
                return BadRequest(new { message = "Rating must be between 1 and 5." });

            var review = new Review
            {
                UserId = dto.UserId,
                BookId = dto.BookId,
                Rating = dto.Rating,
                Comment = dto.Comment
            };

            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Review submitted successfully." });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
                return NotFound(new { message = $"Review with ID {id} not found." });

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
