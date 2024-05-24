namespace HotelBackend.Admin.Application.Dtos.RoomTypes;

public class CreateRoomTypeDto
{
    public string Type { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal? RoomPrice { get; set; }
    public Guid HotelId { get; set; }
}