using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHealthcare.API.Repositories.Interfaces;
using SmartHealthcare.API.Services.Interfaces;
using SmartHealthcare.Core.DTOs;
using SmartHealthcare.Core.Models;

namespace SmartHealthcare.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepository _repo;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly ILogger<DoctorsController> _logger;

        public DoctorsController(IDoctorRepository repo, IMapper mapper,
            ICacheService cache, ILogger<DoctorsController> logger)
        { _repo = repo; _mapper = mapper; _cache = cache; _logger = logger; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            const string key = "doctors_all";
            var cached = _cache.Get<IEnumerable<DoctorDto>>(key);
            if (cached != null) return Ok(cached);
            var doctors = await _repo.GetAllAsync();
            var dto = _mapper.Map<IEnumerable<DoctorDto>>(doctors);
            _cache.Set(key, dto, TimeSpan.FromMinutes(15));
            return Ok(dto);
        }

        [HttpGet("byuser/{userId:int}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var doctors = await _repo.GetAllAsync();
            var doctor = doctors.FirstOrDefault(d => d.UserId == userId);
            if (doctor == null) return NotFound();
            return Ok(_mapper.Map<DoctorDto>(doctor));
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var doctor = await _repo.GetByIdAsync(id);
            if (doctor == null) return NotFound();
            return Ok(_mapper.Map<DoctorDto>(doctor));
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search([FromQuery] string specialization)
        {
            var doctors = await _repo.GetBySpecializationAsync(specialization);
            return Ok(_mapper.Map<IEnumerable<DoctorDto>>(doctors));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateDoctorDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var doctor = _mapper.Map<Doctor>(dto);
            if (dto.SpecializationIds.Any())
                doctor.DoctorSpecializations = dto.SpecializationIds
                    .Select(sid => new DoctorSpecialization { SpecializationId = sid }).ToList();
            var created = await _repo.CreateAsync(doctor);
            _cache.Remove("doctors_all");
            _logger.LogInformation("Doctor created: {Id}", created.Id);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<DoctorDto>(created));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateDoctorDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = await _repo.UpdateAsync(id, _mapper.Map<Doctor>(dto));
            if (updated == null) return NotFound();
            _cache.Remove("doctors_all");
            return Ok(_mapper.Map<DoctorDto>(updated));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repo.DeleteAsync(id);
            if (!result) return NotFound();
            _cache.Remove("doctors_all");
            return NoContent();
        }
    }
}

