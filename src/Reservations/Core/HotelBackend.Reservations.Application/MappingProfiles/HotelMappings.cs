using AutoMapper;
using HotelBackend.Reservations.Application.Dtos.Hotels;
using HotelBackend.Reservations.Domain.Entities.Admin;

namespace HotelBackend.Reservations.Application.MappingProfiles;

public class HotelMappings : Profile
{
    public HotelMappings()
    {
        CreateMap<Hotel, GetHotelResponse>().ReverseMap();
        CreateMap<Hotel, CreateHotelRequest>().ReverseMap();
    }
}