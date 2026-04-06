namespace SmartHealthcare.API.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
    }

    public class RegisterDTO
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }

    public class LoginDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class DoctorDTO
    {
        public int DoctorId { get; set; }
        public string? FullName { get; set; }
        public string? Specialization { get; set; }
        public int ExperienceYears { get; set; }
        public string? Availability { get; set; }
        public string? DepartmentName { get; set; }
    }

    public class AppointmentDTO
    {
        public int AppointmentId { get; set; }
        public string? PatientName { get; set; }
        public string? DoctorName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Status { get; set; }
        public string? Symptoms { get; set; }
    }

    public class CreateAppointmentDTO
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Symptoms { get; set; }
    }

    public class BillDTO
    {
        public int BillId { get; set; }
        public int AppointmentId { get; set; }
        public decimal ConsultationFee { get; set; }
        public decimal MedicineCharges { get; set; }
        public decimal TotalAmount { get; set; }
        public string? PaymentStatus { get; set; }
        public string? Medicines { get; set; }
        public string? DoctorNotes { get; set; }
        public string? PatientName { get; set; }
        public string? Symptoms { get; set; }
    }

    public class PrescriptionDTO
    {
        public int AppointmentId { get; set; }
        public string? Medicines { get; set; }
        public string? Notes { get; set; }
        public decimal MedicineCharges { get; set; }
    }
}
