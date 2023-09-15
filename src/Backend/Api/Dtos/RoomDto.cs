namespace Api.Dtos;

public class RoomDto
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public int RoomNumber { get; set; }
    public int Capacity { get; set; }
    public string RoomType { get; set; }
    public decimal RoomPrice { get; set; }
    public bool? Availabilty { get; set; } = true;
}
