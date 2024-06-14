using System.Text.Json.Serialization;

namespace HotelBackend.Reservations.Application.Dtos.AdminApi.RoomApi;

public record ReserveRoomApiResponse
{
    public bool Success { get; init; }
    [JsonPropertyName("roomId")]
    public Guid? RoomId { get; init; }
}