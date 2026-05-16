using BookingSystem.Api.Models;

namespace BookingSystem.Api.Repositories;

public interface IBookingRepository
{
    Task<IEnumerable<Booking>> GetAllAsync();

    Task<IEnumerable<Booking>> GetByDateAsync(DateTime date);

    Task<Booking?> GetByIdAsync(Guid id);

    Task<Booking> CreateAsync(Booking booking);

    Task UpdateAsync(Booking booking);

    Task DeleteAsync(Guid id);
}