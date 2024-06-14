namespace Hrs.Application.Dtos.Admin.Rooms;

public record ReserveRoomRequest
{
    public Guid HotelId { get; init; }
    public Guid RoomTypeId { get; init; }
}