namespace HotelBackend.Old.Remove.Api.Dtos;

public class ReservationDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public decimal Price { get; set; }
    public decimal Tax { get; set; }
    public double Discount { get; set; }
    public int GuestId { get; set; }
    public int HotelId { get; set; }
    public int NumberOfGuests { get; set; }
}
