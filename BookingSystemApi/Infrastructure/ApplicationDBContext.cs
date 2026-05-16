using BookingSystem.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookingSystem.Api.Infrastructure;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(
        DbContextOptions<ApplicationDBContext> options)
    : base(options)
    {
    }

    public DbSet<Booking> Bookings => Set<Booking>();
}