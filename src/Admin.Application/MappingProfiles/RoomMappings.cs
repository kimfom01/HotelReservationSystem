using Admin.Application.Dtos.Admin.Rooms;
using AutoMapper;
using Admin.Domain.Entities.Admin;

namespace Admin.Application.MappingProfiles;

public class RoomMappings : Profile
{
    public RoomMappings()
    {
        CreateMap<Room, GetRoomResponse>().ReverseMap();
        CreateMap<Room, CreateRoomRequest>().ReverseMap();
        CreateMap<Room, ReserveRoomRequest>().ReverseMap();
    }
}