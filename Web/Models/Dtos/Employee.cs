using System.Text.Json.Serialization;

namespace Web.Models.Dtos;

public class Employee
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string LastName { get; set; }

    [JsonPropertyName("position")]
    public string Position { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("hotelId")]
    public int HotelId { get; set; }

    [JsonPropertyName("hotel")]
    public Hotel? Hotel { get; set; }
}
