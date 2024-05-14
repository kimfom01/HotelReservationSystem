using HotelBackend.Application.Dtos.Common;

namespace HotelBackend.Application.Dtos;

public class RoomTypeDto : BaseDto
{
    public string Type { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Description { get; set; } = string.Empty;
    public PriceDto? Price { get; set; }
}