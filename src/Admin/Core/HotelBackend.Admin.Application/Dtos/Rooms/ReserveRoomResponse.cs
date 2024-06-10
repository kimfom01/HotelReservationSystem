namespace HotelBackend.Admin.Application.Dtos.Rooms;

public record ReserveRoomResponse
{
    public Guid? RoomId { get; init; }
}