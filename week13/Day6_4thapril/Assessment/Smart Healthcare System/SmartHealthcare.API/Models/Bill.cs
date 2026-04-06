namespace SmartHealthcare.API.Models
{
    public class Bill
    {
        public int BillId { get; set; }
        public int AppointmentId { get; set; }
        public decimal ConsultationFee { get; set; }
        public decimal MedicineCharges { get; set; }
        public decimal TotalAmount => ConsultationFee + MedicineCharges;
        public string PaymentStatus { get; set; } = "Unpaid"; // Paid, Unpaid

        // Navigation
        public Appointment? Appointment { get; set; }
    }
}
