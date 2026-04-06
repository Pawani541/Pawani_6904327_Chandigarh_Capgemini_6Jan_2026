using System.ComponentModel.DataAnnotations;

namespace SmartHealthcare.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required] public string Username { get; set; } = "";
        [Required][EmailAddress] public string Email { get; set; } = "";
        [Required] public string PasswordHash { get; set; } = "";
        public string Role { get; set; } = "Patient";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Patient? Patient { get; set; }
    }
}
