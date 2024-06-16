namespace Hrs.Application.Dtos.Admin.Rooms;

public record ReserveRoomResponse
{
    public Guid? RoomId { get; init; }
    public bool Success { get; init; }
}