using HotelBackend.Admin.Application.Dtos.Common;

namespace HotelBackend.Admin.Application.Dtos.RoomTypes;

public class GetRoomTypeDto : BaseDto
{
    public string Type { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal RoomPrice { get; set; }
    public Guid HotelId { get; set; }
}