using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagement.Web.Models.ViewModels;

public class ReservationViewModel
{
    public SelectList? Hotels { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public int HotelId { get; set; }
    public int NumberOfGuests { get; set; }
}
