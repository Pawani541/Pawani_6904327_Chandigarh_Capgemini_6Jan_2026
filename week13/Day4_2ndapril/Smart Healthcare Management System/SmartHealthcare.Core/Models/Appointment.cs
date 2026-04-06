using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHealthcare.Core.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        [Required] public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = "Scheduled";
        public string Notes { get; set; } = "";
        public int PatientId { get; set; }
        [ForeignKey("PatientId")] public Patient Patient { get; set; } = null!;
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")] public Doctor Doctor { get; set; } = null!;
        public Prescription? Prescription { get; set; }
    }
}
