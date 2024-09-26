namespace Admin.Application.Dtos.Admin.RoomTypes;

public record GetRoomTypeRequest
{
    
    public Guid RoomTypeId { get; set; }
    public Guid HotelId { get; set; }
}