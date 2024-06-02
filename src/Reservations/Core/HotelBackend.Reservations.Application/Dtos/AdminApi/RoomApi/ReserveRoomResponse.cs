using System.Text.Json.Serialization;

namespace HotelBackend.Reservations.Application.Dtos.AdminApi.RoomApi;

public class ReserveRoomResponse
{
    public bool Success { get; set; }
    [JsonPropertyName("roomId")]
    public Guid? RoomId { get; set; }
}