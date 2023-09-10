using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.ViewModels;

public class HotelAmenityViewModel
{
    public string? HotelName { get; set; }
    public string? Amenity { get; set; }
    public int HotelId { get; set; }
    public SelectList? Hotels { get; set; }
    public int HotelAmenityId { get; set; }
}
