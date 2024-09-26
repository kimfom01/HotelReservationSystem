using Admin.Application.Dtos.Admin.RoomTypes;
using AutoMapper;
using Admin.Domain.Entities.Admin;

namespace Admin.Application.MappingProfiles;

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