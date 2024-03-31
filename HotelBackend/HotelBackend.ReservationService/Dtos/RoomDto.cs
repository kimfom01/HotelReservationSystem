namespace HotelBackend.ReservationService.Dtos;

public class RoomDto
{
    public Guid Id { get; set; }
    public int RoomNumber { get; set; }
    public bool Availability { get; set; }
    public Guid HotelId { get; set; }
    public Guid RoomTypeId { get; set; }
    public RoomTypeDto? RoomType { get; set; }
}