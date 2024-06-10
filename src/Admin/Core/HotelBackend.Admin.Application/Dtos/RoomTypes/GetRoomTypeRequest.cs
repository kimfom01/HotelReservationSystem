namespace HotelBackend.Admin.Application.Dtos.RoomTypes;

public record GetRoomTypeRequest
{
    
    public Guid RoomTypeId { get; set; }
    public Guid HotelId { get; set; }
}