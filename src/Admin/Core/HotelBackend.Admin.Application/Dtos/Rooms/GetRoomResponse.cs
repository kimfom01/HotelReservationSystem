using HotelBackend.Admin.Application.Dtos.Common;
using HotelBackend.Admin.Application.Dtos.RoomTypes;

namespace HotelBackend.Admin.Application.Dtos.Rooms;

public record GetRoomResponse : BaseDto
{
    public string RoomNumber { get; init; } = string.Empty;
    public bool Availability { get; init; }
    public Guid HotelId { get; init; }
    public Guid RoomTypeId { get; init; }
    public GetRoomTypeResponse? RoomType { get; init; }
}