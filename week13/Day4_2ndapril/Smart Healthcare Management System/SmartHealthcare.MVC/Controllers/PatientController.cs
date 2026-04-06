using Microsoft.AspNetCore.Mvc;
using SmartHealthcare.MVC.Models;
using SmartHealthcare.MVC.Services.Interfaces;

namespace SmartHealthcare.MVC.Controllers
{
    public class PatientController : Controller
    {
        private readonly IApiService _api;
        public PatientController(IApiService api) { _api = api; }

        private bool IsLoggedIn() =>
            HttpContext.Session.GetString("JwtToken") != null;

        private string GetRole() =>
            HttpContext.Session.GetString("Role") ?? "";

        public async Task<IActionResult> Index()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Auth");
            if (GetRole() != "Admin") return RedirectToAction("Index", "Home");
            var patients = await _api.GetAsync<List<PatientViewModel>>("patients") ?? new();
            return View(patients);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Auth");
            if (GetRole() != "Admin") return RedirectToAction("Index", "Home");
            return View(new CreatePatientViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePatientViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // Step 1: Register a user account for the patient
            var userResult = await _api.PostAsync<AuthResponseViewModel>("auth/register", new
            {
                Username = model.FullName.Replace(" ", "").ToLower(),
                Email    = model.Email,
                Password = model.Password,
                Role     = "Patient"
            });

            if (userResult == null)
            {
                ModelState.AddModelError("", "Could not create user account. Email may already exist.");
                return View(model);
            }

            // Step 2: Create the patient profile linked to the user
            var patientResult = await _api.PostAsync<PatientViewModel>("patients", new
            {
                model.FullName,
                model.Phone,
                model.DateOfBirth,
                model.Gender,
                model.Address,
                UserId = userResult.UserId
            });

            if (patientResult == null)
            {
                ModelState.AddModelError("", "User created but patient profile failed.");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Auth");
            await _api.DeleteAsync($"patients/{id}");
            return RedirectToAction("Index");
        }
    }
}
