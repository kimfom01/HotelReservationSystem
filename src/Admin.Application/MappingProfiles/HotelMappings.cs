using Admin.Application.Dtos.Admin.Hotels;
using AutoMapper;
using Admin.Domain.Entities.Admin;

namespace Admin.Application.MappingProfiles;

public class HotelMappings : Profile
{
    public HotelMappings()
    {
        CreateMap<Hotel, GetHotelResponse>().ReverseMap();
        CreateMap<Hotel, CreateHotelRequest>().ReverseMap();
    }
}