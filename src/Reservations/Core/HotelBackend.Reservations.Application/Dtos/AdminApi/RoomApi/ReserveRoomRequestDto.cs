namespace HotelBackend.Reservations.Application.Dtos.AdminApi.RoomApi;

public class ReserveRoomRequestDto
{
    public Guid HotelId { get; set; }
    public Guid RoomTypeId { get; set; }
}