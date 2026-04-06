using Microsoft.AspNetCore.Mvc;
using SmartHealthcare.API.DTOs;
using SmartHealthcare.API.Interfaces;
using SmartHealthcare.API.Models;

namespace SmartHealthcare.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentRepository _repo;
        private readonly IBillRepository _billRepo;

        public AppointmentsController(IAppointmentRepository repo, IBillRepository billRepo)
        {
            _repo = repo;
            _billRepo = billRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var appts = await _repo.GetAllAsync();
            var result = appts.Select(a => new AppointmentDTO
            {
                AppointmentId = a.AppointmentId,
                PatientName = a.Patient?.FullName ?? "",
                DoctorName = a.Doctor?.User?.FullName ?? "",
                AppointmentDate = a.AppointmentDate,
                Status = a.Status,
                Symptoms = a.Symptoms ?? ""
            });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var appt = await _repo.GetByIdAsync(id);
            if (appt == null) return NotFound();
            return Ok(new AppointmentDTO
            {
                AppointmentId = appt.AppointmentId,
                PatientName = appt.Patient?.FullName ?? "",
                DoctorName = appt.Doctor?.User?.FullName ?? "",
                AppointmentDate = appt.AppointmentDate,
                Status = appt.Status,
                Symptoms = appt.Symptoms ?? ""
            });
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatient(int patientId)
        {
            var appts = await _repo.GetByPatientAsync(patientId);
            var result = appts.Select(a => new AppointmentDTO
            {
                AppointmentId = a.AppointmentId,
                PatientName = a.Patient?.FullName ?? "",
                DoctorName = a.Doctor?.User?.FullName ?? "",
                AppointmentDate = a.AppointmentDate,
                Status = a.Status,
                Symptoms = a.Symptoms ?? ""
            });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentDTO dto)
        {
            var appt = new Appointment
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                AppointmentDate = dto.AppointmentDate,
                Status = "Booked",
                Symptoms = dto.Symptoms ?? ""
            };
            await _repo.AddAsync(appt);

            // Check if bill already exists for this appointment
            var existingBill = await _billRepo.GetByAppointmentAsync(appt.AppointmentId);
            if (existingBill == null)
            {
                var bill = new Bill
                {
                    AppointmentId = appt.AppointmentId,
                    ConsultationFee = 500,
                    MedicineCharges = 0,
                    PaymentStatus = "Unpaid"
                };
                await _billRepo.AddAsync(bill);
            }

            return Ok(new { message = "Appointment booked successfully", appt.AppointmentId });
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            var appt = await _repo.GetByIdAsync(id);
            if (appt == null) return NotFound();
            appt.Status = status;
            await _repo.UpdateAsync(appt);
            return Ok(new { message = "Status updated" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return Ok(new { message = "Appointment cancelled" });
        }
    }
}
