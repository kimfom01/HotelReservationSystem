namespace Hrs.Application.Dtos.Admin.Rooms;

public record FreeRoomRequest
{
    public Guid RoomId { get; init; }
    public Guid HotelId { get; init; }
}