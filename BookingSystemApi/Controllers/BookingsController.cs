using AutoMapper;
using BookingSystem.Api.DataTransferObjects;
using BookingSystem.Api.Models;
using BookingSystem.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly BookingService _service;
    private readonly IMapper _mapper;

    public BookingsController(
        BookingService service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookingResponse>>> GetAll()
    {
        var bookings = await _service.GetAllAsync();

        return Ok(_mapper.Map<IEnumerable<BookingResponse>>(bookings));
    }

    [HttpGet("date/{date}")]
    public async Task<ActionResult<IEnumerable<BookingResponse>>> GetByDate(DateTime date)
    {
        var bookings = await _service.GetByDateAsync(date);

        return Ok(_mapper.Map<IEnumerable<BookingResponse>>(bookings));
    }

    [HttpPost]
    public async Task<ActionResult<BookingResponse>> Create(CreateBookingRequest request)
    {
        var booking = _mapper.Map<Booking>(request);

        var createdBooking = await _service.CreateAsync(booking);

        return Ok(_mapper.Map<BookingResponse>(createdBooking));
    }

    [HttpPut("{id}/accept")]
    public async Task<IActionResult> Accept(Guid id)
    {
        await _service.AcceptAsync(id);

        return NoContent();
    }

    [HttpPut("{id}/decline")]
    public async Task<IActionResult> Decline(Guid id)
    {
        await _service.DeclineAsync(id);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}