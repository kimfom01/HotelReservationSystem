namespace HotelBackend.Admin.Application.Dtos.RoomTypes;

public class GetRoomTypeRequestDto
{
    
    public Guid RoomTypeId { get; set; }
    public Guid HotelId { get; set; }
}