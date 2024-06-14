using AutoMapper;
using Hrs.Domain.Entities.Reservation;
using Hrs.Application.Dtos.GuestProfiles;

namespace Hrs.Application.MappingProfiles;

public class GuestProfileMappings : Profile
{
    public GuestProfileMappings()
    {
        CreateMap<GuestProfile, CreateGuestProfileRequest>().ReverseMap();
        CreateMap<GuestProfile, GetGuestProfileResponse>().ReverseMap();
    }
}