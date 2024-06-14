using AutoMapper;
using Hrs.Domain.Entities.Admin;
using Hrs.Application.Dtos.Admin.Hotels;

namespace Hrs.Application.MappingProfiles;

public class HotelMappings : Profile
{
    public HotelMappings()
    {
        CreateMap<Hotel, GetHotelResponse>().ReverseMap();
        CreateMap<Hotel, CreateHotelRequest>().ReverseMap();
    }
}