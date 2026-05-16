using BookingSystem.Api.DataTransferObjects;
using BookingSystem.Api.Models;
using BookingSystem.Api.Repositories;

namespace BookingSystem.Api.Services;

public class BookingService
{
    private readonly IBookingRepository _repository;

    public BookingService(IBookingRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<BookingResponse>> GetAllAsync()
    {
        var bookings = await _repository.GetAllAsync();

        return bookings.Select(Map);
    }

    public async Task<IEnumerable<BookingResponse>> GetByDateAsync(DateTime date)
    {
        var bookings = await _repository.GetByDateAsync(date);

        return bookings.Select(Map);
    }

    public async Task<BookingResponse> CreateAsync(
        CreateBookingRequest request)
    {
        var booking = new Booking
        {
            CustomerName = request.CustomerName,
            Title = request.Title,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };

        var created = await _repository.CreateAsync(booking);

        return Map(created);
    }

    public async Task AcceptAsync(Guid id)
    {
        var booking = await _repository.GetByIdAsync(id);

        if (booking is null)
        {
            throw new Exception("Booking not found");
        }

        booking.Status = BookingStatus.Accepted;

        await _repository.UpdateAsync(booking);
    }

    public async Task DeclineAsync(Guid id)
    {
        var booking = await _repository.GetByIdAsync(id);

        if (booking is null)
        {
            throw new Exception("Booking not found");
        }

        booking.Status = BookingStatus.Declined;

        await _repository.UpdateAsync(booking);
    }

    private static BookingResponse Map(Booking booking)
    {
        return new BookingResponse
        {
            Id = booking.Id,
            CustomerName = booking.CustomerName,
            Title = booking.Title,
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            Status = booking.Status
        };
    }
}