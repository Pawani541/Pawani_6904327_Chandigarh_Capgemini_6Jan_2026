using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MyIISApp.Data;
using MyIISApp.Models;
using MyIISApp.Services;
using BCrypt.Net;

namespace MyIISApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(AppDbContext context)
        {
            _context = context;
            _tokenService = new TokenService();
        }

        // 🔐 REGISTER
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            // 🔥 Default role
            user.Role = "User";

            // 🔥 Hash password
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User Registered");
        }

        // 🔐 LOGIN
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(User user)
        {
            var existingUser = _context.Users
                .FirstOrDefault(x => x.Username == user.Username);

            if (existingUser == null)
                return Unauthorized("Invalid username");

            bool isValid = BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password);

            if (!isValid)
                return Unauthorized("Invalid password");

            // 🔥 TOKEN with ROLE
            var token = _tokenService.CreateToken(existingUser.Username, existingUser.Role);

            return Ok(token);
        }
    }
}