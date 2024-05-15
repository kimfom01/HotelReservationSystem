using AutoMapper;
using HotelBackend.Reservations.Domain.Entities;
using HotelBackend.Reservations.Application.Dtos.GuestProfiles;

namespace HotelBackend.Reservations.Application.MappingProfiles;

public class GuestProfileMappings : Profile
{
    public GuestProfileMappings()
    {
        CreateMap<GuestProfile, CreateGuestProfileDto>().ReverseMap();
        CreateMap<GuestProfile, GetGuestProfileDto>().ReverseMap();
    }
}