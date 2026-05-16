using BookingSystem.Api.Models;
using BookingSystem.Api.Repositories;
using Microsoft.Extensions.Logging;

namespace BookingSystem.Api.Services;

public class BookingService
{
    private readonly IBookingRepository _repository;
    private readonly ILogger<BookingService> _logger;

    public BookingService(
        IBookingRepository repository,
        ILogger<BookingService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        _logger.LogInformation("Fetching all bookings");

        var bookings = await _repository.GetAllAsync();

        _logger.LogInformation("Fetched {Count} bookings", bookings.Count());

        return bookings;
    }

    public async Task<IEnumerable<Booking>> GetByDateAsync(DateTime date)
    {
        _logger.LogInformation("Fetching bookings for date {Date}", date);

        var bookings = await _repository.GetByDateAsync(date);

        if (!bookings.Any())
        {
            _logger.LogWarning("No bookings found for date {Date}", date);
        }

        return bookings;
    }

    public async Task<Booking> CreateAsync(Booking booking)
    {
        _logger.LogInformation(
            "Creating booking for customer {CustomerName}",
            booking.CustomerName);

        if (booking.EndDate <= booking.StartDate)
        {
            _logger.LogWarning(
                "Invalid booking date range. StartDate: {StartDate}, EndDate: {EndDate}",
                booking.StartDate,
                booking.EndDate);

            throw new ArgumentException("End date must be after start date.");
        }

        var createdBooking = await _repository.CreateAsync(booking);

        _logger.LogInformation(
            "Booking created successfully with id {BookingId}",
            createdBooking.Id);

        return createdBooking;
    }

    public async Task AcceptAsync(Guid id)
    {
        _logger.LogInformation("Accepting booking with id {BookingId}", id);

        var booking = await _repository.GetByIdAsync(id);

        if (booking is null)
        {
            _logger.LogError("Booking with id {BookingId} was not found", id);

            throw new KeyNotFoundException("Booking not found.");
        }

        booking.Status = BookingStatus.Accepted;

        await _repository.UpdateAsync(booking);

        _logger.LogInformation("Booking with id {BookingId} accepted", id);
    }

    public async Task DeclineAsync(Guid id)
    {
        _logger.LogInformation("Declining booking with id {BookingId}", id);

        var booking = await _repository.GetByIdAsync(id);

        if (booking is null)
        {
            _logger.LogError("Booking with id {BookingId} was not found", id);

            throw new KeyNotFoundException("Booking not found.");
        }

        booking.Status = BookingStatus.Declined;

        await _repository.UpdateAsync(booking);

        _logger.LogInformation("Booking with id {BookingId} declined", id);
    }

    public async Task DeleteAsync(Guid id)
    {
        _logger.LogInformation("Deleting booking with id {BookingId}", id);

        await _repository.DeleteAsync(id);

        _logger.LogInformation("Booking with id {BookingId} deleted", id);
    }
}