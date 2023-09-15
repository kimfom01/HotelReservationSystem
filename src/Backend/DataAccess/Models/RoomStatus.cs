namespace DataAccess.Models;

public class RoomStatus
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public Room? Room { get; set; }
    public int GuestId { get; set; }
    public Guest? Guest { get; set; }
    public int ReservationId { get; set; }
    public Reservation? Reservation { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
}
