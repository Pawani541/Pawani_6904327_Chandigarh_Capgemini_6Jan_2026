using System.ComponentModel.DataAnnotations;

namespace EventRegistrationRP.Models
{
    public class EventRegistration
    {
        public int Id { get; set; }

        [Required]
        public string ParticipantName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string EventName { get; set; }
    }
}