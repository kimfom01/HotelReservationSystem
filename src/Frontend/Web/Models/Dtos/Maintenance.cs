using System.Text.Json.Serialization;

namespace Web.Models.Dtos;

public class Maintenance
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("roomId")]
    public int RoomId { get; set; }

    [JsonPropertyName("room")]
    public Room? Room { get; set; }

    [JsonPropertyName("maintenanceType")]
    public string MaintenanceType { get; set; }

    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("endDate")]
    public DateTime EndDate { get; set; }
}
