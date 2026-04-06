using System.ComponentModel.DataAnnotations;
namespace EventBooking.API.DTOs;

public class BookingDto
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string EventTitle { get; set; } = string.Empty;
    public int SeatsBooked { get; set; }
    public DateTime BookedOn { get; set; }
}

public class AdminBookingDto
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string EventTitle { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public int SeatsBooked { get; set; }
    public DateTime BookedOn { get; set; }
}

public class CreateBookingDto
{
    [Required] public int EventId { get; set; }
    [Range(1, 100)] public int SeatsBooked { get; set; }
}