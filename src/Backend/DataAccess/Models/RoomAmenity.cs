namespace DataAccess.Models;

public class RoomAmenity
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public Room? Room { get; set; }
    public string Name { get; set; }
}
