using EventBooking.API.DTOs;                     // ← for RegisterDto, LoginDto
using EventBooking.API.Services;                 // ← for AuthService
using Microsoft.AspNetCore.Mvc;  
namespace EventBooking.API.Controllers;
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _auth;
    public AuthController(AuthService auth) => _auth = auth;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var token = await _auth.RegisterAsync(dto);
        if (token == null) return BadRequest("Username already exists.");
        return Ok(new { token });
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        var token = _auth.LoginAsync(dto);
        if (token == null) return Unauthorized("Invalid credentials.");
        return Ok(new { token });
    }
}