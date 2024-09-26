using Admin.Application.Dtos.Admin.RoomTypes;
using Hrs.Common.Dtos;

namespace Admin.Application.Dtos.Admin.Rooms;

public record GetRoomResponse : BaseDto
{
    public string RoomNumber { get; init; } = string.Empty;
    public bool Availability { get; init; }
    public Guid HotelId { get; init; }
    public Guid RoomTypeId { get; init; }
    public GetRoomTypeResponse? RoomType { get; init; }
}