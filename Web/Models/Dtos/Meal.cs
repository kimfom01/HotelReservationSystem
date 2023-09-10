using System.Text.Json.Serialization;

namespace Web.Models.Dtos;

public class Meal
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("mealPrice")]
    public int MealPrice { get; set; }

    [JsonPropertyName("hotelId")]
    public int HotelId { get; set; }

    [JsonPropertyName("hotel")]
    public Hotel? Hotel { get; set; }
}
