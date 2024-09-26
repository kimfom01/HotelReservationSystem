namespace ReservationService.Application.Dtos.Rooms;

public record ReserveRoomResponse
{
    public Guid? RoomId { get; init; }
    public bool Success { get; init; }
}