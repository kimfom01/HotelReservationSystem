namespace HotelBackend.Application.Dtos;

public class RoomTypeDto
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Description { get; set; } = string.Empty;
    public PriceDto? Price { get; set; }
}