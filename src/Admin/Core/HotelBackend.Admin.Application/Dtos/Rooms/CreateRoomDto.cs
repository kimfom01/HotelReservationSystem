namespace HotelBackend.Admin.Application.Dtos.Rooms;

public class CreateRoomDto
{
    public string RoomNumber { get; set; } = string.Empty;
    public Guid HotelId { get; set; }
    public Guid RoomTypeId { get; set; }
}