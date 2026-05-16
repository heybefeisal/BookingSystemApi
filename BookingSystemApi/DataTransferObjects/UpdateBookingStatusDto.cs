using BookingSystem.Api.Models;

namespace BookingSystem.Api.DataTransferObjects;

public class UpdateBookingStatusDto
{
    public BookingStatus Status { get; set; }
}