namespace HotelBackend.Admin.Application.Dtos.Rooms;

public record ReserveRoomRequest
{
    public Guid HotelId { get; init; }
    public Guid RoomTypeId { get; init; }
}