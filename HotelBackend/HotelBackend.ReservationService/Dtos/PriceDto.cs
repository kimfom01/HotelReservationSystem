namespace HotelBackend.ReservationService.Dtos;

public class PriceDto
{
    public Guid Id { get; set; }
    public decimal Value { get; set; }
    public Guid RoomTypeId { get; set; }
}
