using AutoMapper;
using HotelBackend.Application.Dtos;
using HotelBackend.Domain.Entities;

namespace HotelBackend.Application.MappingProfiles;

public class PriceMappings : Profile
{
    public PriceMappings()
    {
        CreateMap<PriceModel, PriceDto>().ReverseMap();
    }
}