using HotelBackend.Admin.Application.Dtos.Common;
using HotelBackend.Admin.Application.Dtos.RoomTypes;

namespace HotelBackend.Admin.Application.Dtos.Rooms;

public class GetRoomDto : BaseDto
{
    public string RoomNumber { get; set; } = string.Empty;
    public bool Availability { get; set; }
    public Guid HotelId { get; set; }
    public Guid RoomTypeId { get; set; }
    public GetRoomTypeDto? RoomType { get; set; }
}