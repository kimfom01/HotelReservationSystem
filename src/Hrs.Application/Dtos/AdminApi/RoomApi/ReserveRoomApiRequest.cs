namespace Hrs.Application.Dtos.AdminApi.RoomApi;

public record ReserveRoomApiRequest
{
    public Guid HotelId { get; init; }
    public Guid RoomTypeId { get; init; }
}