using System.Text.Json.Serialization;

namespace HotelManagement.Web.Models.Dtos;

public class ReservationRoom
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("reservation_Id")]
    public int ReservationId { get; set; }

    [JsonPropertyName("reservation")]
    public Reservation? Reservation { get; set; }

    [JsonPropertyName("roomId")]
    public int RoomId { get; set; }

    [JsonPropertyName("room")]
    public Room? Room { get; set; }
}
