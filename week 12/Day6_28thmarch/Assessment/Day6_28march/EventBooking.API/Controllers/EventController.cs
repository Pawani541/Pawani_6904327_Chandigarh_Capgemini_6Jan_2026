using AutoMapper;
using EventBooking.API.Data;
using EventBooking.API.DTOs;
using EventBooking.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.API.Controllers;
[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public EventsController(AppDbContext db, IMapper mapper)
    { _db = db; _mapper = mapper; }

    // GET api/events
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var events = await _db.Events.ToListAsync();
        return Ok(_mapper.Map<List<EventDto>>(events));
    }

    // GET api/events/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var ev = await _db.Events.FindAsync(id);
        if (ev == null) return NotFound();
        return Ok(_mapper.Map<EventDto>(ev));
    }

    // POST api/events  (Admin can seed events)
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CreateEventDto dto)
    {
        var ev = _mapper.Map<Event>(dto);
        _db.Events.Add(ev);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = ev.Id }, _mapper.Map<EventDto>(ev));
    }
}