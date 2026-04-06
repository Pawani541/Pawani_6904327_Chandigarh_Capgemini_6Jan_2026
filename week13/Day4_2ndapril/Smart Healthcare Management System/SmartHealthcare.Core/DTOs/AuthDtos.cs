using System.ComponentModel.DataAnnotations;

namespace SmartHealthcare.Core.DTOs
{
    public class RegisterDto
    {
        [Required] public string Username { get; set; } = "";
        [Required][EmailAddress] public string Email { get; set; } = "";
        [Required][MinLength(6)] public string Password { get; set; } = "";
        public string Role { get; set; } = "Patient";
    }

    public class LoginDto
    {
        [Required] public string Email { get; set; } = "";
        [Required] public string Password { get; set; } = "";
    }

    public class AuthResponseDto
    {
        public string Token { get; set; } = "";
        public string RefreshToken { get; set; } = "";
        public string Role { get; set; } = "";
        public string Username { get; set; } = "";
        public int UserId { get; set; }
        public int ProfileId { get; set; }
    }
}
