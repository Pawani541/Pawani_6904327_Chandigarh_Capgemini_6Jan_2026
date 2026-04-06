using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHealthcare.Core.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [Required] public string FullName { get; set; } = "";
        [Required] public string Phone { get; set; } = "";
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = "";
        public string Address { get; set; } = "";
        public int UserId { get; set; }
        [ForeignKey("UserId")] public User User { get; set; } = null!;
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
