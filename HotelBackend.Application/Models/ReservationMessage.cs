namespace HotelBackend.Application.Models;

public class ReservationMessage
{
    public Guid Id { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public string SpecialRequests { get; set; }
    public string RoomPreferences { get; set; }
    public int NumberOfGuests { get; set; }
    public Guid GuestProfileId { get; set; }
    public Guid HotelId { get; set; }
    public Guid RoomId { get; set; }
}