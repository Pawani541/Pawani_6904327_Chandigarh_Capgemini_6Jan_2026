using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHealthcare.API.Data;
using SmartHealthcare.API.DTOs;
using SmartHealthcare.API.Interfaces;
using SmartHealthcare.API.Models;

namespace SmartHealthcare.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IAppointmentRepository _apptRepo;
        private readonly IBillRepository _billRepo;
        private readonly AppDbContext _context;

        public PrescriptionsController(IAppointmentRepository apptRepo, IBillRepository billRepo, AppDbContext context)
        {
            _apptRepo = apptRepo;
            _billRepo = billRepo;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddPrescription(PrescriptionDTO dto)
        {
            var prescription = new Prescription
            {
                AppointmentId = dto.AppointmentId,
                Medicines = dto.Medicines,
                Notes = dto.Notes,
                MedicineCharges = dto.MedicineCharges
            };
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            var bill = await _billRepo.GetByAppointmentAsync(dto.AppointmentId);
            if (bill != null)
            {
                bill.MedicineCharges = dto.MedicineCharges;
                await _billRepo.UpdateAsync(bill);
            }

            var appt = await _apptRepo.GetByIdAsync(dto.AppointmentId);
            if (appt != null)
            {
                appt.Status = "Completed";
                await _apptRepo.UpdateAsync(appt);
            }

            return Ok(new { message = "Prescription saved and bill updated" });
        }

        [HttpGet("appointment/{appointmentId}")]
        public async Task<IActionResult> GetByAppointment(int appointmentId)
        {
            var prescription = await _context.Prescriptions
                .FirstOrDefaultAsync(p => p.AppointmentId == appointmentId);
            if (prescription == null) return NotFound(new { message = "No prescription yet" });
            return Ok(prescription);
        }
    }
}
