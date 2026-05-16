using BookingSystem.Api.DataTransferObjects;
using BookingSystem.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly BookingService _service;

    public BookingsController(BookingService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("date/{date}")]
    public async Task<IActionResult> GetByDate(DateTime date)
    {
        return Ok(await _service.GetByDateAsync(date));
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateBookingRequest request)
    {
        var booking = await _service.CreateAsync(request);

        return Ok(booking);
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
}