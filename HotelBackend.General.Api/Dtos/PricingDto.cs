namespace HotelBackend.General.Api.Dtos;

public class PricingDto
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public DateTime Date { get; set; }
    public int NumberOfGuests { get; set; }
    public decimal Price { get; set; }
}
