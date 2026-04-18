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
    [Route("api/v{version:apiVersion}/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthorsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _context.Authors
                .Select(a => new AuthorDto { AuthorId = a.AuthorId, Name = a.Name })
                .ToListAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return NotFound(new { message = $"Author with ID {id} not found." });
            return Ok(new AuthorDto { AuthorId = author.AuthorId, Name = author.Name });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] AuthorDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest(new { message = "Author name is required." });

            var author = new Author { Name = dto.Name };
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = author.AuthorId },
                new AuthorDto { AuthorId = author.AuthorId, Name = author.Name });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] AuthorDto dto)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return NotFound(new { message = $"Author with ID {id} not found." });

            author.Name = dto.Name;
            await _context.SaveChangesAsync();
            return Ok(new AuthorDto { AuthorId = author.AuthorId, Name = author.Name });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return NotFound(new { message = $"Author with ID {id} not found." });

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
