namespace HotelBackend.Admin.Application.Dtos.Rooms;

public class UpdateRoomAvailabilityDto
{
    public bool Availability { get; set; }
    public Guid HotelId { get; set; }
    public Guid RoomId { get; set; }
}