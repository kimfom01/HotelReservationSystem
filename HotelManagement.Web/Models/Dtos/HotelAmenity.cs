using System.Text.Json.Serialization;

namespace HotelManagement.Web.Models.Dtos;

public class HotelAmenity
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("hotelId")]
    public int HotelId { get; set; }

    [JsonPropertyName("hotel")]
    public Hotel? Hotel { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}
