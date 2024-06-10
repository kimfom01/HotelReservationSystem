using AutoMapper;
using HotelBackend.Admin.Application.Dtos.Hotels;
using HotelBackend.Admin.Domain.Entities;

namespace HotelBackend.Admin.Application.MappingProfiles;

public class HotelMappings : Profile
{
    public HotelMappings()
    {
        CreateMap<Hotel, GetHotelResponse>().ReverseMap();
        CreateMap<Hotel, CreateHotelRequest>().ReverseMap();
    }
}