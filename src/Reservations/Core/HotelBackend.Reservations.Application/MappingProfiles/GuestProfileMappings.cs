using AutoMapper;
using HotelBackend.Reservations.Application.Dtos.GuestProfiles;
using HotelBackend.Reservations.Domain.Entities.Reservation;

namespace HotelBackend.Reservations.Application.MappingProfiles;

public class GuestProfileMappings : Profile
{
    public GuestProfileMappings()
    {
        CreateMap<GuestProfile, CreateGuestProfileRequest>().ReverseMap();
        CreateMap<GuestProfile, GetGuestProfileResponse>().ReverseMap();
    }
}