using Microsoft.AspNetCore.Mvc;
using SmartHealthcare.MVC.Models;
using SmartHealthcare.MVC.Services.Interfaces;

namespace SmartHealthcare.MVC.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IApiService _api;
        public AppointmentController(IApiService api) { _api = api; }

        private bool IsLoggedIn() =>
            HttpContext.Session.GetString("JwtToken") != null;

        private string GetRole() =>
            HttpContext.Session.GetString("Role") ?? "";

        private int GetUserId() =>
            HttpContext.Session.GetInt32("UserId") ?? 0;

        public async Task<IActionResult> Index()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Auth");

            var role   = GetRole();
            var userId = GetUserId();
            List<AppointmentViewModel>? appts;

            if (role == "Patient")
            {
                // Resolve PatientId from UserId
                var patient = await _api.GetAsync<PatientViewModel>($"patients/byuser/{userId}");
                if (patient == null)
                {
                    ViewBag.Role = role;
                    ViewBag.Error = "No patient profile found for your account.";
                    return View(new List<AppointmentViewModel>());
                }
                appts = await _api.GetAsync<List<AppointmentViewModel>>($"appointments/patient/{patient.Id}");
            }
            else if (role == "Doctor")
            {
                // Resolve DoctorId from UserId
                var doctor = await _api.GetAsync<DoctorViewModel>($"doctors/byuser/{userId}");
                if (doctor == null)
                {
                    ViewBag.Role = role;
                    ViewBag.Error = "No doctor profile found for your account. Please ask Admin to create your doctor profile.";
                    return View(new List<AppointmentViewModel>());
                }
                appts = await _api.GetAsync<List<AppointmentViewModel>>($"appointments/doctor/{doctor.Id}");
            }
            else
            {
                appts = await _api.GetAsync<List<AppointmentViewModel>>("appointments");
            }

            ViewBag.Role = role;
            return View(appts ?? new List<AppointmentViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Auth");

            var role   = GetRole();
            var userId = GetUserId();

            ViewBag.Doctors  = await _api.GetAsync<List<DoctorViewModel>>("doctors") ?? new();
            ViewBag.Patients = await _api.GetAsync<List<PatientViewModel>>("patients") ?? new();
            ViewBag.Role     = role;

            var model = new CreateAppointmentViewModel();

            if (role == "Patient")
            {
                // Resolve the Patient profile Id (not UserId)
                var patient = await _api.GetAsync<PatientViewModel>($"patients/byuser/{userId}");
                model.PatientId = patient?.Id ?? 0;
            }

            if (role == "Doctor")
            {
                // Resolve the Doctor profile Id (not UserId)
                var doctor = await _api.GetAsync<DoctorViewModel>($"doctors/byuser/{userId}");
                ViewBag.DoctorProfileId = doctor?.Id ?? 0;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentViewModel model)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Auth");

            if (!ModelState.IsValid)
            {
                var role   = GetRole();
                var userId = GetUserId();
                ViewBag.Doctors  = await _api.GetAsync<List<DoctorViewModel>>("doctors") ?? new();
                ViewBag.Patients = await _api.GetAsync<List<PatientViewModel>>("patients") ?? new();
                ViewBag.Role     = role;
                return View(model);
            }

            await _api.PostAsync<AppointmentViewModel>("appointments", model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateStatus(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Auth");
            if (GetRole() != "Doctor" && GetRole() != "Admin") return Forbid();

            var appt = await _api.GetAsync<AppointmentViewModel>($"appointments/{id}");
            if (appt == null) return NotFound();
            return View(appt);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Auth");
            if (GetRole() != "Doctor" && GetRole() != "Admin") return Forbid();
            await _api.PatchAsync<AppointmentViewModel>($"appointments/{id}/status", new { Status = status });
            return RedirectToAction("Index");
        }
    }
}

