using System.ComponentModel.DataAnnotations;

namespace SmartHealthcare.API.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = "Booked";
        public string? Symptoms { get; set; }

        public User? Patient { get; set; }
        public Doctor? Doctor { get; set; }
        public Prescription? Prescription { get; set; }
        public Bill? Bill { get; set; }
    }
}
