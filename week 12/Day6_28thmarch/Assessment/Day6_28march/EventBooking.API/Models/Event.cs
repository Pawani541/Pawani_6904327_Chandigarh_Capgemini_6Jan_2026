using System.ComponentModel.DataAnnotations;
using EventBooking.API.Validations;

namespace EventBooking.API.Models;

public class Event
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [FutureDate(ErrorMessage = "Event date must be in the future")]
    public DateTime Date { get; set; }

    public string Location { get; set; } = string.Empty;

    [Range(1, 10000, ErrorMessage = "Available seats must be between 1 and 10000")]
    public int AvailableSeats { get; set; }

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}