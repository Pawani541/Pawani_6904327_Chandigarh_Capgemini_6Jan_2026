using BookStore.Application.DTOs;
using BookStore.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthApiService _authService;

        public AccountController(IAuthApiService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var result = await _authService.LoginAsync(dto);
            if (result == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(dto);
            }

            HttpContext.Session.SetString("JwtToken", result.Token);
            HttpContext.Session.SetString("UserName", result.FullName);
            HttpContext.Session.SetString("UserEmail", result.Email);
            HttpContext.Session.SetString("UserRole", result.Role);

            return RedirectToAction("Index", "Books");
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var success = await _authService.RegisterAsync(dto);
            if (!success)
            {
                ModelState.AddModelError("", "Registration failed. Email may already be in use.");
                return View(dto);
            }

            TempData["Success"] = "Registration successful! Please login.";
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
