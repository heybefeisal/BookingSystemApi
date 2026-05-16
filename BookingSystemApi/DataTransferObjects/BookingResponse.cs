using BookingSystem.Api.Models;

namespace BookingSystem.Api.DataTransferObjects;

public class BookingResponse
{
    public Guid Id { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public BookingStatus Status { get; set; }
}