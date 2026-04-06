using System.ComponentModel.DataAnnotations;

namespace SmartHealthcare.Core.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        [Required] public string FullName { get; set; } = "";
        [Required][EmailAddress] public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public int ExperienceYears { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<DoctorSpecialization> DoctorSpecializations { get; set; } = new List<DoctorSpecialization>();
    }
}
