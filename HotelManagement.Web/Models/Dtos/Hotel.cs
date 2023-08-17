using System.Text.Json.Serialization;

namespace HotelManagement.Web.Models.Dtos;

public class Hotel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("location")]
    public string Location { get; set; }
}
