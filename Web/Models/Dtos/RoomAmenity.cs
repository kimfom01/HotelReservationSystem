using System.Text.Json.Serialization;

namespace Web.Models.Dtos;

public class RoomAmenity
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("roomId")]
    public int RoomId { get; set; }

    [JsonPropertyName("room")]
    public Room? Room { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}
