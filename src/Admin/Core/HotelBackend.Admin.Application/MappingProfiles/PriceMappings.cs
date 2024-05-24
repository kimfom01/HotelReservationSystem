using AutoMapper;
using HotelBackend.Admin.Application.Dtos;
using HotelBackend.Admin.Domain.Entities;

namespace HotelBackend.Admin.Application.MappingProfiles;

public class PriceMappings : Profile
{
    public PriceMappings()
    {
        CreateMap<PriceDto, RoomPrice>().ReverseMap();
    }
}