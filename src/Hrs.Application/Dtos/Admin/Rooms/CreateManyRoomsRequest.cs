namespace Hrs.Application.Dtos.Admin.Rooms;

public record CreateManyRoomsRequest
{
    public int Count { get; init; }
    public int Start { get; set; }
    public Guid RoomTypeId { get; init; }
    public Guid HotelId { get; init; }
}