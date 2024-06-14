namespace HotelBackend.Reservations.Application.Dtos.Admin.Rooms;

public record CreateRoomRequest
{
    public string RoomNumber { get; init; } = string.Empty;
    public Guid HotelId { get; init; }
    public Guid RoomTypeId { get; init; }
}