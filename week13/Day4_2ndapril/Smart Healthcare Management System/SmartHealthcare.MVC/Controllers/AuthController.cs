using Microsoft.AspNetCore.Mvc;
using SmartHealthcare.MVC.Models;
using SmartHealthcare.MVC.Services.Interfaces;

namespace SmartHealthcare.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IApiService _api;
        public AuthController(IApiService api) { _api = api; }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _api.PostAsync<AuthResponseViewModel>("auth/login", new
            {
                model.Email,
                model.Password
            });

            if (result == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            HttpContext.Session.SetString("JwtToken",  result.Token);
            HttpContext.Session.SetString("Username",  result.Username);
            HttpContext.Session.SetString("Role",      result.Role);
            HttpContext.Session.SetInt32("UserId",     result.UserId);
            HttpContext.Session.SetInt32("ProfileId",  result.ProfileId);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _api.PostAsync<AuthResponseViewModel>("auth/register", new
            {
                model.Username,
                model.Email,
                model.Password,
                model.Role
            });

            if (result == null)
            {
                ModelState.AddModelError("", "Registration failed. Email may already be in use.");
                return View(model);
            }

            // If registering as Patient, auto-create Patient profile
            if (model.Role == "Patient")
            {
                _api.SetToken(result.Token);
                await _api.PostAsync<PatientViewModel>("patients", new
                {
                    FullName    = model.Username,
                    Phone       = "0000000000",
                    DateOfBirth = DateTime.Today.AddYears(-25),
                    Gender      = "Not specified",
                    Address     = "",
                    UserId      = result.UserId
                });

                // Re-login to get updated ProfileId
                var refreshed = await _api.PostAsync<AuthResponseViewModel>("auth/login", new
                {
                    model.Email,
                    model.Password
                });
                if (refreshed != null) result = refreshed;
            }

            HttpContext.Session.SetString("JwtToken",  result.Token);
            HttpContext.Session.SetString("Username",  result.Username);
            HttpContext.Session.SetString("Role",      result.Role);
            HttpContext.Session.SetInt32("UserId",     result.UserId);
            HttpContext.Session.SetInt32("ProfileId",  result.ProfileId);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
