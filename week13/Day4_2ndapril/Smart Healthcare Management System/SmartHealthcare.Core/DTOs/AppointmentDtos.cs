using System.ComponentModel.DataAnnotations;

namespace SmartHealthcare.Core.DTOs
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = "";
        public string Notes { get; set; } = "";
        public int PatientId { get; set; }
        public string PatientName { get; set; } = "";
        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = "";
    }

    public class CreateAppointmentDto
    {
        [Required] public DateTime AppointmentDate { get; set; }
        public string Notes { get; set; } = "";
        [Required] public int PatientId { get; set; }
        [Required] public int DoctorId { get; set; }
    }

    public class UpdateAppointmentStatusDto
    {
        [Required] public string Status { get; set; } = "";
    }
}
