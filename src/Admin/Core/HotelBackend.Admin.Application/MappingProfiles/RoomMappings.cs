using AutoMapper;
using HotelBackend.Admin.Application.Dtos.Rooms;
using HotelBackend.Admin.Domain.Entities;

namespace HotelBackend.Admin.Application.MappingProfiles;

public class RoomMappings : Profile
{
    public RoomMappings()
    {
        CreateMap<Room, GetRoomResponse>().ReverseMap();
        CreateMap<Room, CreateRoomRequest>().ReverseMap();
        CreateMap<Room, ReserveRoomRequest>().ReverseMap();
    }
}