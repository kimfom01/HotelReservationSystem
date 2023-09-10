using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.ViewModels;

public class MaintenanceViewModel
{
    public int MaintenanceId { get; set; }
    public int RoomId { get; set; }
    public int RoomNumber { get; set; }
    public SelectList? Rooms { get; set; }
    public string MaintenanceType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
