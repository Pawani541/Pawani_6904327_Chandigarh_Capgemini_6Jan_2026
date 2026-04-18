using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.Infrastructure.Data;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Asp.Versioning;

namespace BookStore.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public AuthController(AppDbContext context, ITokenService tokenService, IEmailService emailService)
        {
            _context = context;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest(new { message = "Email already registered." });

            var passwordHash = HashPassword(dto.Password);

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = passwordHash,
                Phone = dto.Phone,
                RoleId = 2
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var profile = new UserProfile
            {
                UserId = user.UserId,
                Address = dto.Address,
                City = dto.City,
                Pincode = dto.Pincode
            };

            await _context.UserProfiles.AddAsync(profile);
            await _context.SaveChangesAsync();

            await _emailService.SendWelcomeEmailAsync(user.Email, user.FullName);

            return Ok(new { message = "Registration successful." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || user.PasswordHash != HashPassword(dto.Password))
                return Unauthorized(new { message = "Invalid email or password." });

            var token = _tokenService.GenerateJwtToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            return Ok(new AuthResponseDto
            {
                Token = token,
                RefreshToken = refreshToken,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.RoleName,
                Expiry = DateTime.UtcNow.AddHours(2)
            });
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
