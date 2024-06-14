using HotelBackend.Reservations.Application.Dtos.Common;

namespace HotelBackend.Reservations.Application.Dtos.RoomTypes;

public record GetRoomTypeResponse : BaseDto
{
    public string Type { get; init; } = string.Empty;
    public int Capacity { get; init; }
    public string Description { get; init; } = string.Empty;
    public decimal RoomPrice { get; init; }
    public Guid HotelId { get; init; }
}