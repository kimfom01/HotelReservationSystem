using AutoMapper;
using ReservationService.Application.Dtos.GuestProfiles;
using ReservationService.Domain;

namespace ReservationService.Application.MappingProfiles;

public class GuestProfileMappings : Profile
{
    public GuestProfileMappings()
    {
        CreateMap<GuestProfile, CreateGuestProfileRequest>().ReverseMap();
        CreateMap<GuestProfile, GetGuestProfileResponse>().ReverseMap();
    }
}