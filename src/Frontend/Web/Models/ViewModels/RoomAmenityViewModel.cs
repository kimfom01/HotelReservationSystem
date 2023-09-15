using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.ViewModels;

public class RoomAmenityViewModel
{
    public int AmenityId { get; set; }
    public string RoomType { get; set; }
    public string Name { get; set; }
    public int RoomNumber { get; set; }
    public SelectList? Rooms { get; set; }
    public int RoomId { get; set; }
}
