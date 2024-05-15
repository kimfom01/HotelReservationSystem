using AutoMapper;
using HotelBackend.Reservations.Domain.Entities;
using HotelBackend.Reservations.Application.Dtos;

namespace HotelBackend.Reservations.Application.MappingProfiles;

public class HotelMappings : Profile
{
    public HotelMappings()
    {
        CreateMap<Hotel, HotelDto>().ReverseMap();
    }
}