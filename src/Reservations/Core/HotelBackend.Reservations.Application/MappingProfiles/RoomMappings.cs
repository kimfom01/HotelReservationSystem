using AutoMapper;
using HotelBackend.Reservations.Application.Dtos.Admin.Rooms;
using HotelBackend.Reservations.Domain.Entities.Admin;

namespace HotelBackend.Reservations.Application.MappingProfiles;

public class RoomMappings : Profile
{
    public RoomMappings()
    {
        CreateMap<Room, GetRoomResponse>().ReverseMap();
        CreateMap<Room, CreateRoomRequest>().ReverseMap();
        CreateMap<Room, ReserveRoomRequest>().ReverseMap();
    }
}