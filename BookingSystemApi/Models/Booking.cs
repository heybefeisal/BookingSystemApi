namespace BookingSystem.Api.Models;

public class Booking
{
    public Guid Id { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public BookingStatus Status { get; set; } = BookingStatus.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}