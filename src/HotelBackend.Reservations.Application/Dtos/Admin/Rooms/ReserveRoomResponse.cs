namespace HotelBackend.Reservations.Application.Dtos.Admin.Rooms;

public record ReserveRoomResponse
{
    public Guid? RoomId { get; init; }
}