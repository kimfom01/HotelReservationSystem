namespace HotelBackend.ReservationService.Dtos;

public class RoomTypeDto
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public int Capacity { get; set; }
    public string Description { get; set; }
    public PriceDto? Price { get; set; }
}