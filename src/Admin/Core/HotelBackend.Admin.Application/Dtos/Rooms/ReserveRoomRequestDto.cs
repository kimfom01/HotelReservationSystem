namespace HotelBackend.Admin.Application.Dtos.Rooms;

public class ReserveRoomRequestDto
{
    public Guid HotelId { get; set; }
    public Guid RoomTypeId { get; set; }
}