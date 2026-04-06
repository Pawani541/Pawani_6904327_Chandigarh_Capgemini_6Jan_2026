using System.ComponentModel.DataAnnotations;

namespace SmartHealthcare.MVC.Models
{
    public class LoginViewModel
    {
        [Required][EmailAddress]
        public string Email { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";
    }

    public class RegisterViewModel
    {
        [Required] public string Username { get; set; } = "";
        [Required][EmailAddress] public string Email { get; set; } = "";
        [Required][MinLength(6)] public string Password { get; set; } = "";
        public string Role { get; set; } = "Patient";
    }

    public class AuthResponseViewModel
    {
        public string Token { get; set; } = "";
        public string RefreshToken { get; set; } = "";
        public string Role { get; set; } = "";
        public string Username { get; set; } = "";
        public int UserId { get; set; }
    public int ProfileId { get; set; }
    }

    public class AppointmentViewModel
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

    public class CreateAppointmentViewModel
    {
        [Required] public DateTime AppointmentDate { get; set; } = DateTime.Now;
        public string Notes { get; set; } = "";
        [Required] public int PatientId { get; set; }
        [Required] public int DoctorId { get; set; }
    }

    public class DoctorViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public int ExperienceYears { get; set; }
        public List<string> Specializations { get; set; } = new();
    }

    public class PatientViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string Phone { get; set; } = "";
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = "";
        public string Address { get; set; } = "";
        public int UserId { get; set; }
    public int ProfileId { get; set; }
    }

    public class CreatePatientViewModel
    {
        [Required] public string FullName { get; set; } = "";
        [Required][Phone] public string Phone { get; set; } = "";
        [Required] public DateTime DateOfBirth { get; set; } = DateTime.Today.AddYears(-25);
        [Required] public string Gender { get; set; } = "";
        public string Address { get; set; } = "";
        [Required][EmailAddress] public string Email { get; set; } = "";
        [Required][MinLength(6)] public string Password { get; set; } = "";
    }
}

