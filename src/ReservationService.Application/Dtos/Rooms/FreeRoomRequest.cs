namespace ReservationService.Application.Dtos.Rooms;

public record FreeRoomRequest
{
    public Guid RoomId { get; init; }
    public Guid HotelId { get; init; }
}