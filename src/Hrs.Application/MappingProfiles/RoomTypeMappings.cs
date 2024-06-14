using AutoMapper;
using Hrs.Domain.Entities.Admin;
using Hrs.Application.Dtos.Admin.RoomTypes;

namespace Hrs.Application.MappingProfiles;

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