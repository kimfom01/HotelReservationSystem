using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.ViewModels;

public class RoomViewModel
{
    public string? HotelName { get; set; }
    public int HotelId { get; set; }
    public SelectList? Hotels { get; set; }
    public int RoomNumber { get; set; }
    public int Capacity { get; set; }
    public string? RoomType { get; set; }
    public decimal RoomPrice { get; set; }
    public bool? Availabilty { get; set; } = true;
    public int RoomId { get; set; }
}
