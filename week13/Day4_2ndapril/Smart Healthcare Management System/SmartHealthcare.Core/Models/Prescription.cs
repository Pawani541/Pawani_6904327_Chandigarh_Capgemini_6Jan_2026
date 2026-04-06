using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHealthcare.Core.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        [Required] public string Notes { get; set; } = "";
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public int AppointmentId { get; set; }
        [ForeignKey("AppointmentId")] public Appointment Appointment { get; set; } = null!;
        public ICollection<PrescriptionMedicine> PrescriptionMedicines { get; set; } = new List<PrescriptionMedicine>();
    }
}
