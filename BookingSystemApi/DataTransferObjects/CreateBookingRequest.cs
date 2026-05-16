namespace BookingSystem.Api.DataTransferObjects;

public class CreateBookingRequest
{
    public string CustomerName { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}