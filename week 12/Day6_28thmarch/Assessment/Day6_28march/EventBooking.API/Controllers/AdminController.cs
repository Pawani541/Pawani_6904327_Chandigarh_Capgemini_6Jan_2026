using AutoMapper;
using EventBooking.API.Data;
using EventBooking.API.DTOs;
using EventBooking.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.API.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize]
public class AdminController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public AdminController(AppDbContext db, IMapper mapper)
    { _db = db; _mapper = mapper; }

    private bool IsAdmin()
    {
        var claim = User.FindFirst("IsAdmin");
        return claim != null && claim.Value == "True";
    }

    // POST api/admin/events — Create event (admin only)
    [HttpPost("events")]
    public async Task<IActionResult> CreateEvent(CreateEventDto dto)
    {
        if (!IsAdmin()) return Forbid();
        var ev = _mapper.Map<Event>(dto);
        _db.Events.Add(ev);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetEvent), new { id = ev.Id }, _mapper.Map<EventDto>(ev));
    }

    // GET api/admin/events/{id}
    [HttpGet("events/{id}")]
    public async Task<IActionResult> GetEvent(int id)
    {
        if (!IsAdmin()) return Forbid();
        var ev = await _db.Events.FindAsync(id);
        if (ev == null) return NotFound();
        return Ok(_mapper.Map<EventDto>(ev));
    }

    // PUT api/admin/events/{id} — Update event (admin only)
    [HttpPut("events/{id}")]
    public async Task<IActionResult> UpdateEvent(int id, CreateEventDto dto)
    {
        if (!IsAdmin()) return Forbid();
        var ev = await _db.Events.FindAsync(id);
        if (ev == null) return NotFound();
        _mapper.Map(dto, ev);
        await _db.SaveChangesAsync();
        return Ok(_mapper.Map<EventDto>(ev));
    }

    // DELETE api/admin/events/{id} — Delete event (admin only)
    [HttpDelete("events/{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        if (!IsAdmin()) return Forbid();
        var ev = await _db.Events.FindAsync(id);
        if (ev == null) return NotFound();
        _db.Events.Remove(ev);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    // GET api/admin/bookings — All bookings with username (admin only)
    [HttpGet("bookings")]
    public async Task<IActionResult> AllBookings()
    {
        if (!IsAdmin()) return Forbid();

        var bookings = await _db.Bookings
            .Include(b => b.Event)
            .ToListAsync();

        var userIds = bookings.Select(b => b.UserId).Distinct().ToList();
        var users   = await _db.Users
            .Where(u => userIds.Contains(u.Id))
            .ToDictionaryAsync(u => u.Id, u => u.Username);

        var result = bookings.Select(b => new AdminBookingDto
        {
            Id         = b.Id,
            EventId    = b.EventId,
            EventTitle = b.Event?.Title ?? "",
            UserId     = b.UserId,
            Username   = users.TryGetValue(b.UserId, out var name) ? name : "Unknown",
            SeatsBooked = b.SeatsBooked,
            BookedOn   = b.BookedOn
        });

        return Ok(result);
    }

    // GET api/admin/users — All users (admin only)
    [HttpGet("users")]
    public async Task<IActionResult> AllUsers()
    {
        if (!IsAdmin()) return Forbid();
        var users = await _db.Users.Select(u => new {
            u.Id, u.Username, u.Email, u.IsAdmin
        }).ToListAsync();
        return Ok(users);
    }
}
