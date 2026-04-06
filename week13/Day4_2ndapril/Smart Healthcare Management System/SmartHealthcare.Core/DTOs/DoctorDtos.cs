using System.ComponentModel.DataAnnotations;

namespace SmartHealthcare.Core.DTOs
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public int ExperienceYears { get; set; }
        public List<string> Specializations { get; set; } = new();
    }

    public class CreateDoctorDto
    {
        [Required] public string FullName { get; set; } = "";
        [Required][EmailAddress] public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        [Range(0, 50)] public int ExperienceYears { get; set; }
        public List<int> SpecializationIds { get; set; } = new();
    }
}
