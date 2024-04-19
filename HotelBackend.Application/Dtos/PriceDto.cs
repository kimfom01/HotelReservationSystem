using HotelBackend.Application.Dtos.Common;

namespace HotelBackend.Application.Dtos;

public class PriceDto : BaseDto
{
    public decimal Value { get; set; }
    public Guid RoomTypeId { get; set; }
}