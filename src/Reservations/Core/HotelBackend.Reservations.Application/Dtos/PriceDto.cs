using HotelBackend.Reservations.Application.Dtos.Common;

namespace HotelBackend.Reservations.Application.Dtos;

public class PriceDto : BaseDto
{
    public decimal Value { get; set; }
    public Guid RoomTypeId { get; set; }
}