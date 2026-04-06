using System.ComponentModel.DataAnnotations;

namespace SmartHealthcare.API.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Doctor? Doctor { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
