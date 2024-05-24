using HotelBackend.Admin.Application.Dtos.Common;

namespace HotelBackend.Admin.Application.Dtos;

public class PriceDto : BaseDto
{
    public decimal Value { get; set; }
    public Guid RoomTypeId { get; set; }
}