using System.Text.Json.Serialization;

namespace HotelManagement.Web.Models.Dtos;

public class Room
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("hotelId")]
    public int HotelId { get; set; }

    [JsonPropertyName("hotel")]
    public Hotel? Hotel { get; set; }

    [JsonPropertyName("roomNumber")]
    public int RoomNumber { get; set; }

    [JsonPropertyName("capacity")]
    public int Capacity { get; set; }

    [JsonPropertyName("roomType")]
    public string RoomType { get; set; }

    [JsonPropertyName("roomPrice")]
    public decimal RoomPrice { get; set; }

    [JsonPropertyName("availabilty")]
    public bool Availabilty { get; set; }
}
