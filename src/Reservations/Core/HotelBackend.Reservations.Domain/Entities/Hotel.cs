namespace HotelBackend.Reservations.Domain.Entities;

public class Hotel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;

    public IEnumerable<Room>? Rooms { get; set; }
}