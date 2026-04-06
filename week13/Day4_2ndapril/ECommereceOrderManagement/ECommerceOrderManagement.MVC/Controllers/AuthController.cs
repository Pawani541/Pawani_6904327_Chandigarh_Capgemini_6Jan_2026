using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ECommerceOrderManagement.MVC.Models;

namespace ECommerceOrderManagement.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _factory;
        public AuthController(IHttpClientFactory factory) { _factory = factory; }

        public IActionResult Login() => View();
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var client = _factory.CreateClient("API");
            var json = JsonSerializer.Serialize(new { model.Email, model.Password });
            var response = await client.PostAsync("/api/auth/login",
                new StringContent(json, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode) { ViewBag.Error = "Invalid credentials"; return View(); }
            var body = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<JsonElement>(body);
            var token = result.GetProperty("accessToken").GetString()!;

            // Decode token to get name and role
            var parts = token.Split('.');
            var payload = parts[1];
            var padded = payload + new string('=', (4 - payload.Length % 4) % 4);
            var decoded = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(padded));
            var tokenData = JsonSerializer.Deserialize<JsonElement>(decoded);
            var name = tokenData.TryGetProperty("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", out var n) ? n.GetString()! : model.Email;
            var role = tokenData.TryGetProperty("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", out var r) ? r.GetString()! : "User";
            var userId = tokenData.TryGetProperty("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", out var u) ? u.GetString()! : "1";

            HttpContext.Session.SetString("JwtToken", token);
            HttpContext.Session.SetString("UserName", name);
            HttpContext.Session.SetString("UserRole", role);
            HttpContext.Session.SetString("UserId", userId);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.NameIdentifier, userId)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var client = _factory.CreateClient("API");
            var json = JsonSerializer.Serialize(new { model.Name, model.Email, model.Password, model.Role });
            var response = await client.PostAsync("/api/auth/register",
                new StringContent(json, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode) { ViewBag.Error = "Registration failed"; return View(); }
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
