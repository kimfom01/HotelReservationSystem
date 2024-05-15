using HotelBackend.EmailClient.Application.Dtos.Common;

namespace HotelBackend.EmailClient.Application.Dtos;

public class PriceDto : BaseDto
{
    public decimal Value { get; set; }
    public Guid RoomTypeId { get; set; }
}