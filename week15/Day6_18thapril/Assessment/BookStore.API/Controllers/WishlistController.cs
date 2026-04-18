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
    [Route("api/v{version:apiVersion}/wishlist")]
    [Authorize]
    public class WishlistController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WishlistController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var wishlist = await _context.Wishlists
                .Include(w => w.Book)
                .Where(w => w.UserId == userId)
                .Select(w => new WishlistDto
                {
                    UserId = w.UserId,
                    BookId = w.BookId,
                    BookTitle = w.Book.Title,
                    Price = w.Book.Price,
                    ImageUrl = w.Book.ImageUrl
                })
                .ToListAsync();
            return Ok(wishlist);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] WishlistDto dto)
        {
            var exists = await _context.Wishlists
                .AnyAsync(w => w.UserId == dto.UserId && w.BookId == dto.BookId);

            if (exists)
                return BadRequest(new { message = "Book already in wishlist." });

            var wishlist = new Wishlist { UserId = dto.UserId, BookId = dto.BookId };
            await _context.Wishlists.AddAsync(wishlist);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Book added to wishlist." });
        }

        [HttpDelete("{userId}/{bookId}")]
        public async Task<IActionResult> Remove(int userId, int bookId)
        {
            var wishlist = await _context.Wishlists
                .FirstOrDefaultAsync(w => w.UserId == userId && w.BookId == bookId);

            if (wishlist == null)
                return NotFound(new { message = "Wishlist item not found." });

            _context.Wishlists.Remove(wishlist);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
