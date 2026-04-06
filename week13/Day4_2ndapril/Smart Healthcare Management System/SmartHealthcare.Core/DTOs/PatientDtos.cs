using System.ComponentModel.DataAnnotations;

namespace SmartHealthcare.Core.DTOs
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string Phone { get; set; } = "";
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = "";
        public string Address { get; set; } = "";
        public int UserId { get; set; }
    }

    public class CreatePatientDto
    {
        [Required] public string FullName { get; set; } = "";
        [Required] public string Phone { get; set; } = "";
        [Required] public DateTime DateOfBirth { get; set; }
        [Required] public string Gender { get; set; } = "";
        public string Address { get; set; } = "";
        [Required] public int UserId { get; set; }
    }
}
