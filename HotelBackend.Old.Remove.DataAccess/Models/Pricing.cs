namespace HotelBackend.Old.Remove.DataAccess.Models;

public class Pricing
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public Room? Room { get; set; }
    public DateTime Date { get; set; }
    public int NumberOfGuests { get; set; }
    public decimal Price { get; set; }
}
