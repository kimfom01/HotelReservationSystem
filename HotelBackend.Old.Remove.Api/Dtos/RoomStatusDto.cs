namespace HotelBackend.Old.Remove.Api.Dtos;

public class RoomStatusDto
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public int GuestId { get; set; }
    public int ReservationId { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
}
