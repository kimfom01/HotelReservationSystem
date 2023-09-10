using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.ViewModels;

public class PricingViewModel
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public SelectList? Rooms { get; set; }
    public DateTime Date { get; set; }
    public int NumberOfGuests { get; set; }
    public decimal Price { get; set; }
}
