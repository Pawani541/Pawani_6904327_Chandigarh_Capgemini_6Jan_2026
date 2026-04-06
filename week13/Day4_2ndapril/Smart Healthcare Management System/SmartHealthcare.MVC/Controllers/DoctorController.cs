using Microsoft.AspNetCore.Mvc;
using SmartHealthcare.MVC.Models;
using SmartHealthcare.MVC.Services.Interfaces;

namespace SmartHealthcare.MVC.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IApiService _api;
        public DoctorController(IApiService api) { _api = api; }

        public async Task<IActionResult> Index()
        {
            var doctors = await _api.GetAsync<List<DoctorViewModel>>("doctors") ?? new();
            return View(doctors);
        }

        public async Task<IActionResult> Details(int id)
        {
            var doctor = await _api.GetAsync<DoctorViewModel>($"doctors/{id}");
            if (doctor == null) return NotFound();
            return View(doctor);
        }
    }
}
