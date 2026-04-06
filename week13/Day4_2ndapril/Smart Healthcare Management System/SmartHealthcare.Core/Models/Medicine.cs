using System.ComponentModel.DataAnnotations;

namespace SmartHealthcare.Core.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public ICollection<PrescriptionMedicine> PrescriptionMedicines { get; set; } = new List<PrescriptionMedicine>();
    }
}
