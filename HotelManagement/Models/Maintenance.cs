namespace HotelManagement.Models;

public class Maintenance
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public Room? Room { get; set; }
    public string MaintenanceType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
