using Microsoft.AspNetCore.Mvc;
using SmartHealthcare.API.DTOs;
using SmartHealthcare.API.Interfaces;
using SmartHealthcare.API.Models;

namespace SmartHealthcare.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepository _repo;
        public DoctorsController(IDoctorRepository repo) { _repo = repo; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var doctors = await _repo.GetAllAsync();
            var result = doctors.Select(d => new DoctorDTO
            {
                DoctorId = d.DoctorId,
                FullName = d.User?.FullName ?? "",
                Specialization = d.Specialization ?? "",
                ExperienceYears = d.ExperienceYears,
                Availability = d.Availability ?? "",
                DepartmentName = d.Department?.DepartmentName ?? ""
            });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var doctor = await _repo.GetByIdAsync(id);
            if (doctor == null) return NotFound();
            return Ok(doctor);
        }

        [HttpGet("department/{deptId}")]
        public async Task<IActionResult> GetByDepartment(int deptId)
        {
            var doctors = await _repo.GetByDepartmentAsync(deptId);
            return Ok(doctors);
        }
[HttpPost]
        public async Task<IActionResult> Add(Doctor doctor)
        {
            await _repo.AddAsync(doctor);
            return Ok(new { message = "Doctor added", doctor.DoctorId });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return Ok(new { message = "Doctor removed" });
        }
    }
}
