namespace HotelBackend.ReservationService.Dtos;

public class ReservationDto
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public string BookingId { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public decimal Price { get; set; }
    public decimal Tax { get; set; }
    public double Discount { get; set; }
    public string GuestEmail { get; set; }
    public string GuestName { get; set; }
    public int HotelId { get; set; }
    public Guid RoomId { get; set; }
    public int NumberOfGuests { get; set; }
}
