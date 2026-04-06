namespace SmartHealthcare.API.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public int AppointmentId { get; set; }
        public string? Medicines { get; set; }
        public string? Notes { get; set; }
        public decimal MedicineCharges { get; set; } = 0;

        public Appointment? Appointment { get; set; }
    }
}
