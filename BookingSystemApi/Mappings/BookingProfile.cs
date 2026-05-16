using AutoMapper;
using BookingSystem.Api.DataTransferObjects;
using BookingSystem.Api.Models;

namespace BookingSystem.Api.Mappings;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<Booking, BookingResponse>();

        CreateMap<CreateBookingRequest, Booking>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
    }
}