using AutoMapper;
using HotelBackend.ReservationService.Dtos;
using HotelBackend.ReservationService.Models;

namespace HotelBackend.ReservationService.Mapping;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<Hotel, HotelDto>().ReverseMap();
        CreateMap<Pricing, PricingDto>().ReverseMap();
        CreateMap<Reservation, ReservationDto>().ReverseMap();
        CreateMap<Room, RoomDto>().ReverseMap();
    }
}
