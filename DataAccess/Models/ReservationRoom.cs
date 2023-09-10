namespace DataAccess.Models;

public class ReservationRoom
{
    public int Id { get; set; }
    public int ReservationId { get; set; }
    public Reservation? Reservation { get; set; }
    public int RoomId { get; set; }
    public Room? Room { get; set; }
}
