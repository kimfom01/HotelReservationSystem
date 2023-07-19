namespace HotelManagement.Models;

public class ReservationRoom
{
    public int Id { get; set; }
    public int Reservation_Id { get; set; }
    public Reservation? Reservation { get; set; }
    public int RoomId { get; set; }
    public Room? Room { get; set; }
}
