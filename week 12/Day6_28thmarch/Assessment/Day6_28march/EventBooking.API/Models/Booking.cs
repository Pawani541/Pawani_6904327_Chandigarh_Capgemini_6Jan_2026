using System.ComponentModel.DataAnnotations; 
namespace EventBooking.API.Models;

public class Booking
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string UserId { get; set; } = string.Empty;

    [Range(1, 100, ErrorMessage = "Seats booked must be between 1 and 100")]
    public int SeatsBooked { get; set; }

    public DateTime BookedOn { get; set; } = DateTime.UtcNow;

    public Event? Event { get; set; }
}