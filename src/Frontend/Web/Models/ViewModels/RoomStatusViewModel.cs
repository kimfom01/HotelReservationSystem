using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.ViewModels;

public class RoomStatusViewModel
{
    public int RoomStatusId { get; set; }
    public string RoomType { get; set; }
    public int RoomId { get; set; }
    public SelectList? Rooms { get; set; }
    public string GuestName { get; set; }
    public int GuestId { get; set; }
    public SelectList? Guests { get; set; }
    public int ReservationId { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
}
