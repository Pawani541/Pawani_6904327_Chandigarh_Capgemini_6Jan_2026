using System.Security.Claims;
using AutoMapper;
using EventBooking.API.Data;
using EventBooking.API.DTOs;
using EventBooking.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace EventBooking.API.Controllers;

[ApiController]
[Route("api/bookings")]
[Authorize]           // All endpoints require JWT
public class BookingsController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public BookingsController(AppDbContext db, IMapper mapper)
    { _db = db; _mapper = mapper; }

    // POST api/bookings
    [HttpPost]
    public async Task<IActionResult> Book(CreateBookingDto dto)
    {
        var ev = await _db.Events.FindAsync(dto.EventId);
        if (ev == null) return NotFound("Event not found.");
        if (ev.AvailableSeats < dto.SeatsBooked)
            return BadRequest("Not enough seats available.");

        var userId  = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var booking = _mapper.Map<Booking>(dto);
        booking.UserId = userId;

        ev.AvailableSeats -= dto.SeatsBooked;   // reduce available seats
        _db.Bookings.Add(booking);
        await _db.SaveChangesAsync();

        await _db.Entry(booking).Reference(b => b.Event).LoadAsync();
        return Ok(_mapper.Map<BookingDto>(booking));
    }

    // GET api/bookings  (user's own bookings)
    [HttpGet]
    public async Task<IActionResult> MyBookings()
    {
        var userId   = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var bookings = await _db.Bookings
                                .Include(b => b.Event)
                                .Where(b => b.UserId == userId)
                                .ToListAsync();
        return Ok(_mapper.Map<List<BookingDto>>(bookings));
    }

    // DELETE api/bookings/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Cancel(int id)
    {
        var userId  = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var booking = await _db.Bookings.Include(b => b.Event)
                                        .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
        if (booking == null) return NotFound("Booking not found.");

        booking.Event!.AvailableSeats += booking.SeatsBooked; // restore seats
        _db.Bookings.Remove(booking);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}