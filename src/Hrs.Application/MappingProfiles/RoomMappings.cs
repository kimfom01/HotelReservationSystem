using AutoMapper;
using Hrs.Domain.Entities.Admin;
using Hrs.Application.Dtos.Admin.Rooms;

namespace Hrs.Application.MappingProfiles;

public class RoomMappings : Profile
{
    public RoomMappings()
    {
        CreateMap<Room, GetRoomResponse>().ReverseMap();
        CreateMap<Room, CreateRoomRequest>().ReverseMap();
        CreateMap<Room, ReserveRoomRequest>().ReverseMap();
    }
}