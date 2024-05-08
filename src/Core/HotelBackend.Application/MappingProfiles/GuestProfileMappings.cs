using AutoMapper;
using HotelBackend.Application.Dtos.GuestProfiles;
using HotelBackend.Domain.Entities;

namespace HotelBackend.Application.MappingProfiles;

public class GuestProfileMappings : Profile
{
    public GuestProfileMappings()
    {
        CreateMap<GuestProfile, CreateGuestProfileDto>().ReverseMap();
        CreateMap<GuestProfile, GetGuestProfileDto>().ReverseMap();
    }
}