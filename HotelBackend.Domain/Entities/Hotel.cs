namespace HotelBackend.Domain.Entities;

public class Hotel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }

    public IEnumerable<Reservation>? Reservations { get; set; }
    public IEnumerable<Room> Rooms { get; set; }
    
}
