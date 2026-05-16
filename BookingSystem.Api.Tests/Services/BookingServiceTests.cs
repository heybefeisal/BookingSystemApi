using BookingSystem.Api.Models;
using BookingSystem.Api.Repositories;
using BookingSystem.Api.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BookingSystem.Api.Tests.Services;

public class BookingServiceTests
{
    private readonly Mock<IBookingRepository> _repositoryMock;
    private readonly Mock<ILogger<BookingService>> _loggerMock;
    private readonly BookingService _service;

    public BookingServiceTests()
    {
        _repositoryMock = new Mock<IBookingRepository>();
        _loggerMock = new Mock<ILogger<BookingService>>();

        _service = new BookingService(
            _repositoryMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateBooking_WhenDatesAreValid()
    {
        var booking = new Booking
        {
            CustomerName = "John Doe",
            Title = "Test Booking",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddHours(1)
        };

        _repositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<Booking>()))
            .ReturnsAsync(booking);

        var result = await _service.CreateAsync(booking);

        result.Should().NotBeNull();
        result.CustomerName.Should().Be("John Doe");

        _repositoryMock.Verify(
            x => x.CreateAsync(It.IsAny<Booking>()),
            Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowArgumentException_WhenEndDateIsBeforeStartDate()
    {
        var booking = new Booking
        {
            CustomerName = "John Doe",
            Title = "Invalid Booking",
            StartDate = DateTime.UtcNow.AddHours(2),
            EndDate = DateTime.UtcNow
        };

        var act = async () => await _service.CreateAsync(booking);

        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("End date must be after start date.");
    }

    [Fact]
    public async Task AcceptAsync_ShouldSetStatusToAccepted_WhenBookingExists()
    {
        var bookingId = Guid.NewGuid();

        var booking = new Booking
        {
            Id = bookingId,
            CustomerName = "Jane Doe",
            Title = "Booking",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddHours(1),
            Status = BookingStatus.Pending
        };

        _repositoryMock
            .Setup(x => x.GetByIdAsync(bookingId))
            .ReturnsAsync(booking);

        await _service.AcceptAsync(bookingId);

        booking.Status.Should().Be(BookingStatus.Accepted);

        _repositoryMock.Verify(
            x => x.UpdateAsync(booking),
            Times.Once);
    }

    [Fact]
    public async Task DeclineAsync_ShouldSetStatusToDeclined_WhenBookingExists()
    {
        var bookingId = Guid.NewGuid();

        var booking = new Booking
        {
            Id = bookingId,
            CustomerName = "Jane Doe",
            Title = "Booking",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddHours(1),
            Status = BookingStatus.Pending
        };

        _repositoryMock
            .Setup(x => x.GetByIdAsync(bookingId))
            .ReturnsAsync(booking);

        await _service.DeclineAsync(bookingId);

        booking.Status.Should().Be(BookingStatus.Declined);

        _repositoryMock.Verify(
            x => x.UpdateAsync(booking),
            Times.Once);
    }

    [Fact]
    public async Task AcceptAsync_ShouldThrowKeyNotFoundException_WhenBookingDoesNotExist()
    {
        var bookingId = Guid.NewGuid();

        _repositoryMock
            .Setup(x => x.GetByIdAsync(bookingId))
            .ReturnsAsync((Booking?)null);

        var act = async () => await _service.AcceptAsync(bookingId);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage("Booking not found.");
    }
}