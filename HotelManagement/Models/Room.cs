namespace HotelManagement.Models;

public class Room
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public Hotel? Hotel { get; set; }
    public int RoomNumber { get; set; }
    public string RoomType { get; set; }
    public decimal RoomPrice { get; set; }
    public bool Availabilty { get; set; }
}
