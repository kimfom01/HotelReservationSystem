namespace HotelBackend.Reservations.Application.Dtos.Rooms;

public record ReserveRoomResponse
{
    public Guid? RoomId { get; init; }
}