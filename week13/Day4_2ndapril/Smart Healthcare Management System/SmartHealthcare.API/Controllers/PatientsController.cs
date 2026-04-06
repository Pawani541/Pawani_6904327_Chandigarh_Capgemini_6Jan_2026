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
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _repo;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(IPatientRepository repo, IMapper mapper,
            ICacheService cache, ILogger<PatientsController> logger)
        { _repo = repo; _mapper = mapper; _cache = cache; _logger = logger; }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            const string key = "patients_all";
            var cached = _cache.Get<IEnumerable<PatientDto>>(key);
            if (cached != null) return Ok(cached);
            var patients = await _repo.GetAllAsync();
            var dto = _mapper.Map<IEnumerable<PatientDto>>(patients);
            _cache.Set(key, dto);
            return Ok(dto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await _repo.GetByIdAsync(id);
            if (patient == null) return NotFound();
            return Ok(_mapper.Map<PatientDto>(patient));
        }

        [HttpGet("byuser/{userId:int}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var patients = await _repo.GetAllAsync();
            var patient = patients.FirstOrDefault(p => p.UserId == userId);
            if (patient == null) return NotFound();
            return Ok(_mapper.Map<PatientDto>(patient));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePatientDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var patient = _mapper.Map<Patient>(dto);
            var created = await _repo.CreateAsync(patient);
            _cache.Remove("patients_all");
            _logger.LogInformation("Patient created: {Id}", created.Id);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<PatientDto>(created));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreatePatientDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = await _repo.UpdateAsync(id, _mapper.Map<Patient>(dto));
            if (updated == null) return NotFound();
            _cache.Remove("patients_all");
            return Ok(_mapper.Map<PatientDto>(updated));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repo.DeleteAsync(id);
            if (!result) return NotFound();
            _cache.Remove("patients_all");
            return NoContent();
        }
    }
}

