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
    [Route("api/v{version:apiVersion}/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _context.Categories
                .Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name
                })
                .ToListAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound(new { message = $"Category with ID {id} not found." });
            return Ok(new CategoryDto { CategoryId = category.CategoryId, Name = category.Name });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CategoryDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest(new { message = "Category name is required." });

            var category = new Category { Name = dto.Name };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = category.CategoryId },
                new CategoryDto { CategoryId = category.CategoryId, Name = category.Name });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDto dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound(new { message = $"Category with ID {id} not found." });

            category.Name = dto.Name;
            await _context.SaveChangesAsync();
            return Ok(new CategoryDto { CategoryId = category.CategoryId, Name = category.Name });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound(new { message = $"Category with ID {id} not found." });

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
