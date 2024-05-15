using AutoMapper;
using HotelBackend.Reservations.Domain.Entities;
using HotelBackend.Reservations.Application.Dtos;

namespace HotelBackend.Reservations.Application.MappingProfiles;

public class PriceMappings : Profile
{
    public PriceMappings()
    {
        CreateMap<PriceModel, PriceDto>().ReverseMap();
    }
}