using System.Text.Json.Serialization;

namespace HotelBackend.Reservations.Application.Dtos.AdminApi.RoomApi;

public class UpdateRoomAvailabilityDto
{
    [JsonPropertyName("availability")]
    public bool Availability { get; set; }
    [JsonPropertyName("hotelId")]
    public Guid HotelId { get; set; }
    [JsonPropertyName("roomId")]
    public Guid RoomId { get; set; }
}