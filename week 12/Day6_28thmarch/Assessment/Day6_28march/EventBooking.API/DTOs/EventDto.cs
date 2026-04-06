using System.ComponentModel.DataAnnotations;
using EventBooking.API.Validations;
namespace EventBooking.API.DTOs;

public class EventDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Location { get; set; } = string.Empty;
    public int AvailableSeats { get; set; }
}

public class CreateEventDto
{
    [Required] public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [FutureDate] public DateTime Date { get; set; }
    public string Location { get; set; } = string.Empty;
    [Range(1, 10000)] public int AvailableSeats { get; set; }
}