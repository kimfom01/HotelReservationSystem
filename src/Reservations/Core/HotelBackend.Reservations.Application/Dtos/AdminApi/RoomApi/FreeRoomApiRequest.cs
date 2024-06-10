using System.Text.Json.Serialization;

namespace HotelBackend.Reservations.Application.Dtos.AdminApi.RoomApi;

public record FreeRoomApiRequest
{
    [JsonPropertyName("roomId")]
    public Guid RoomId { get; init; }
    [JsonPropertyName("hotelId")]
    public Guid HotelId { get; init; }
}