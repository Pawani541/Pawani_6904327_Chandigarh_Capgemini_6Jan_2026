using System.ComponentModel.DataAnnotations;

namespace SmartHealthcare.API.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }

        [StringLength(100)]
        public string? Specialization { get; set; }
        public int ExperienceYears { get; set; }
        public string? Availability { get; set; }

        // Navigation
        public User? User { get; set; }
        public Department? Department { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
