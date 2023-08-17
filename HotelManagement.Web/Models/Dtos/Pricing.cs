using System.Text.Json.Serialization;

namespace HotelManagement.Web.Models.Dtos;

public class Pricing
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("roomId")]
    public int RoomId { get; set; }

    [JsonPropertyName("room")]
    public Room? Room { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("numberOfGuests")]
    public int NumberOfGuests { get; set; }

    [JsonPropertyName("price")]
    public int Price { get; set; }
}
