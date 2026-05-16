using BookingSystem.Api.Infrastructure;
using BookingSystem.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Api.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly ApplicationDBContext _context;

    public BookingRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        return await _context.Bookings
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Booking>> GetByDateAsync(DateTime date)
    {
        return await _context.Bookings
            .AsNoTracking()
            .Where(x =>
                x.StartDate.Date <= date.Date &&
                x.EndDate.Date >= date.Date)
            .ToListAsync();
    }

    public async Task<Booking?> GetByIdAsync(Guid id)
    {
        return await _context.Bookings
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Booking> CreateAsync(Booking booking)
    {
        _context.Bookings.Add(booking);

        await _context.SaveChangesAsync();

        return booking;
    }

    public async Task UpdateAsync(Booking booking)
    {
        _context.Bookings.Update(booking);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var booking = await _context.Bookings
            .FirstOrDefaultAsync(x => x.Id == id);

        if (booking is null)
        {
            return;
        }

        _context.Bookings.Remove(booking);

        await _context.SaveChangesAsync();
    }
}