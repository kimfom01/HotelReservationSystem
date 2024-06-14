namespace HotelBackend.Reservations.Application.Dtos.Admin.RoomTypes;

public record CreateRoomTypeRequest
{
    public string Type { get; init; } = string.Empty;
    public int Capacity { get; init; }
    public string Description { get; init; } = string.Empty;
    public decimal? RoomPrice { get; init; }
    public Guid HotelId { get; init; }
}