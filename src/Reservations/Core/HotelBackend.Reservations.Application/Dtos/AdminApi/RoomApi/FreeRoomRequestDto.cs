using System.Text.Json.Serialization;

namespace HotelBackend.Reservations.Application.Dtos.AdminApi.RoomApi;

public class FreeRoomRequestDto
{
    [JsonPropertyName("roomId")]
    public Guid RoomId { get; set; }
    [JsonPropertyName("hotelId")]
    public Guid HotelId { get; set; }
}