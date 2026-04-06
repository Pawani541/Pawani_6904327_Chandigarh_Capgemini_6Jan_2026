using Microsoft.AspNetCore.Mvc;
using log4net;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private static readonly ILog log = LogManager.GetLogger(typeof(UserController));

    [HttpPost("login")]
    public IActionResult Login(string email, string password)
    {
        log.Info($"Login attempt: {email}");

        try
        {
            if (string.IsNullOrEmpty(email))
            {
                log.Warn("Email is empty");
                return BadRequest("Email required");
            }

            if (email != "test@gmail.com" || password != "1234")
            {
                log.Warn("Invalid password");
                return Unauthorized();
            }

            return Ok("Login successful");
        }
        catch (Exception ex)
        {
            log.Error("Login error", ex);
            return StatusCode(500);
        }
    }
}