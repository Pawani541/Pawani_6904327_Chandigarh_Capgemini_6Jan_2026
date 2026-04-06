using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHealthcare.API.Repositories.Interfaces;
using SmartHealthcare.Core.DTOs;
using SmartHealthcare.Core.Models;

namespace SmartHealthcare.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<AppointmentsController> _logger;

        public AppointmentsController(IAppointmentRepository repo, IMapper mapper,
            ILogger<AppointmentsController> logger)
        { _repo = repo; _mapper = mapper; _logger = logger; }

        [HttpGet]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> GetAll([FromQuery] DateTime? date)
        {
            var appts = date.HasValue
                ? await _repo.GetByDateAsync(date.Value)
                : await _repo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<AppointmentDto>>(appts));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var appt = await _repo.GetByIdAsync(id);
            if (appt == null) return NotFound();
            return Ok(_mapper.Map<AppointmentDto>(appt));
        }

        [HttpGet("patient/{patientId:int}")]
        public async Task<IActionResult> GetByPatient(int patientId)
        {
            var appts = await _repo.GetByPatientAsync(patientId);
            return Ok(_mapper.Map<IEnumerable<AppointmentDto>>(appts));
        }

        [HttpGet("doctor/{doctorId:int}")]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> GetByDoctor(int doctorId)
        {
            var appts = await _repo.GetByDoctorAsync(doctorId);
            return Ok(_mapper.Map<IEnumerable<AppointmentDto>>(appts));
        }

        [HttpGet("doctor/byuser/{userId:int}")]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> GetByDoctorUserId(int userId)
        {
            var appts = await _repo.GetByDoctorUserIdAsync(userId);
            return Ok(_mapper.Map<IEnumerable<AppointmentDto>>(appts));
        }

        [HttpPost]
        [Authorize(Roles = "Patient,Admin")]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var appt = _mapper.Map<Appointment>(dto);
            var created = await _repo.CreateAsync(appt);
            _logger.LogInformation("Appointment booked: PatientId={P}, DoctorId={D}", dto.PatientId, dto.DoctorId);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<AppointmentDto>(created));
        }

        [HttpPatch("{id:int}/status")]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateAppointmentStatusDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = await _repo.UpdateStatusAsync(id, dto.Status);
            if (updated == null) return NotFound();
            return Ok(_mapper.Map<AppointmentDto>(updated));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repo.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

