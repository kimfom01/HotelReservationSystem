using System.Text.Json.Serialization;

namespace Hrs.Application.Dtos.AdminApi.RoomApi;

public record ReserveRoomApiResponse
{
    public bool Success { get; init; }
    [JsonPropertyName("roomId")]
    public Guid? RoomId { get; init; }
}