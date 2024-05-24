using AutoMapper;
using HotelBackend.Admin.Application.Dtos;
using HotelBackend.Admin.Domain.Entities;

namespace HotelBackend.Admin.Application.MappingProfiles;

public class HotelMappings : Profile
{
    public HotelMappings()
    {
        CreateMap<Hotel, HotelDto>().ReverseMap();
    }
}