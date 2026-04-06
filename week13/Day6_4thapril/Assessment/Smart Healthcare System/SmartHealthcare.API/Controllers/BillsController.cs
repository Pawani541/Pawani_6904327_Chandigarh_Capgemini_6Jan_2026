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
    public class BillsController : ControllerBase
    {
        private readonly IBillRepository _repo;
        private readonly AppDbContext _context;

        public BillsController(IBillRepository repo, AppDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bills = await _context.Bills
                .Include(b => b.Appointment).ThenInclude(a => a.Patient)
                .Include(b => b.Appointment).ThenInclude(a => a.Prescription)
                .ToListAsync();

            var result = bills.Select(b => new BillDTO
            {
                BillId = b.BillId,
                AppointmentId = b.AppointmentId,
                ConsultationFee = b.ConsultationFee,
                MedicineCharges = b.MedicineCharges,
                TotalAmount = b.ConsultationFee + b.MedicineCharges,
                PaymentStatus = b.PaymentStatus,
                PatientName = b.Appointment != null && b.Appointment.Patient != null ? b.Appointment.Patient.FullName : "",
                Symptoms = b.Appointment != null ? b.Appointment.Symptoms ?? "" : "",
                Medicines = b.Appointment != null && b.Appointment.Prescription != null ? b.Appointment.Prescription.Medicines ?? "" : "",
                DoctorNotes = b.Appointment != null && b.Appointment.Prescription != null ? b.Appointment.Prescription.Notes ?? "" : ""
            });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bill = await _repo.GetByIdAsync(id);
            if (bill == null) return NotFound();
            return Ok(bill);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatient(int patientId)
        {
            // Get all appointment IDs for this patient first
            var patientAppointmentIds = await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .Select(a => a.AppointmentId)
                .ToListAsync();

            if (!patientAppointmentIds.Any())
                return Ok(new List<BillDTO>());

            // Get bills for those appointments
            var bills = await _context.Bills
                .Include(b => b.Appointment).ThenInclude(a => a.Patient)
                .Include(b => b.Appointment).ThenInclude(a => a.Prescription)
                .Where(b => patientAppointmentIds.Contains(b.AppointmentId))
                .ToListAsync();

            var result = bills.Select(b => new BillDTO
            {
                BillId = b.BillId,
                AppointmentId = b.AppointmentId,
                ConsultationFee = b.ConsultationFee,
                MedicineCharges = b.MedicineCharges,
                TotalAmount = b.ConsultationFee + b.MedicineCharges,
                PaymentStatus = b.PaymentStatus,
                PatientName = b.Appointment != null && b.Appointment.Patient != null ? b.Appointment.Patient.FullName : "",
                Symptoms = b.Appointment != null ? b.Appointment.Symptoms ?? "" : "",
                Medicines = b.Appointment != null && b.Appointment.Prescription != null ? b.Appointment.Prescription.Medicines ?? "" : "Awaiting prescription",
                DoctorNotes = b.Appointment != null && b.Appointment.Prescription != null ? b.Appointment.Prescription.Notes ?? "" : ""
            });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Bill bill)
        {
            var existing = await _repo.GetByAppointmentAsync(bill.AppointmentId);
            if (existing != null)
                return BadRequest(new { message = "Bill already exists for this appointment" });
            await _repo.AddAsync(bill);
            return Ok(new { message = "Bill created", bill.BillId });
        }

        [HttpPut("{id}/pay")]
        public async Task<IActionResult> MarkAsPaid(int id)
        {
            var bill = await _repo.GetByIdAsync(id);
            if (bill == null) return NotFound();
            bill.PaymentStatus = "Paid";
            await _repo.UpdateAsync(bill);
            return Ok(new { message = "Payment recorded" });
        }
    }
}
