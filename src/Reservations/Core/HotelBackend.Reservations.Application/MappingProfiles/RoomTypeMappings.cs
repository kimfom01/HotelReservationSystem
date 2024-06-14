using AutoMapper;
using HotelBackend.Reservations.Application.Dtos.Admin.RoomTypes;
using HotelBackend.Reservations.Domain.Entities.Admin;

namespace HotelBackend.Reservations.Application.MappingProfiles;

public class RoomTypeMappings : Profile
{
    public RoomTypeMappings()
    {
        CreateMap<RoomType, CreateRoomTypeRequest>()
            .ReverseMap();
        CreateMap<RoomType, GetRoomTypeResponse>()
            .ReverseMap();
    }
}