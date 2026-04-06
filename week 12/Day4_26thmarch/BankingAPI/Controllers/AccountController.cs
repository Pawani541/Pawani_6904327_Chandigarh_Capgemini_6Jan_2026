using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankingAPI.Data;
using BankingAPI.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BankingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AccountController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // 🔹 LOGIN API (JWT TOKEN)
       [HttpPost("login")]
public IActionResult Login(string role)
{
    var claims = new[]
    {
        new Claim(ClaimTypes.Name, "User1"),
        new Claim(ClaimTypes.Role, role)
    };

    // 🔹 Key from appsettings
    var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes("MySuperSecretKey1234567890123456"));

    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: "BankingAPI",
        audience: "BankingUsers",
        claims: claims,
        expires: DateTime.Now.AddHours(1),
        signingCredentials: creds);

    return Ok(new
    {
        token = new JwtSecurityTokenHandler().WriteToken(token)
    });
}

        // 🔹 SECURE API (ROLE-BASED)
        [HttpGet("details")]
        [Authorize]   // ⚠️ secure endpoint
        public async Task<IActionResult> GetAccountDetails()
        {
            var account = await _context.Accounts.FirstOrDefaultAsync();

            if (account == null)
                return NotFound("No account found");

            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (role == "Admin")
            {
                var adminData = _mapper.Map<AdminAccountDTO>(account);
                return Ok(adminData);
            }
            else
            {
                var userData = _mapper.Map<UserAccountDTO>(account);
                return Ok(userData);
            }
        }
    }
}