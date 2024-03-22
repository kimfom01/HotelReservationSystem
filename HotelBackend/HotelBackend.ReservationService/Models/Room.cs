namespace HotelBackend.ReservationService.Models;

public class Room
{
    public Guid Id { get; set; }
    public int HotelId { get; set; }
    public Hotel? Hotel { get; set; }
    public int RoomNumber { get; set; }
    public int Capacity { get; set; }
    public string RoomType { get; set; }
    public decimal RoomPrice { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public bool? Availabilty { get; set; } = true;
    public bool? Payed { get; set; } = false;
}
