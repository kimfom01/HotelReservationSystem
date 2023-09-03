using System.Text.Json.Serialization;

namespace HotelManagement.Web.Models.Dtos;

public class RoomStatus
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("roomId")]
    public int RoomId { get; set; }

    [JsonPropertyName("room")]
    public Room? Room { get; set; }

    [JsonPropertyName("guestId")]
    public int GuestId { get; set; }

    [JsonPropertyName("guest")]
    public Guest? Guest { get; set; }

    [JsonPropertyName("reservationId")]
    public int ReservationId { get; set; }

    [JsonPropertyName("reservation")]
    public Reservation? Reservation { get; set; }

    [JsonPropertyName("checkIn")]
    public DateTime CheckIn { get; set; }

    [JsonPropertyName("checkOut")]
    public DateTime CheckOut { get; set; }
}
