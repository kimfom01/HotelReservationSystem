using AutoMapper;
using HotelBackend.Application.Dtos;
using HotelBackend.Domain.Entities;

namespace HotelBackend.Application.MappingProfiles;

public class HotelMappings : Profile
{
    public HotelMappings()
    {
        CreateMap<Hotel, HotelDto>().ReverseMap();
    }
}