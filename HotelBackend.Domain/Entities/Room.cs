namespace HotelBackend.Domain.Entities;

public class Room
{
    public Guid Id { get; set; }
    public string RoomNumber { get; set; }
    public bool? Availability { get; set; } = true;
    public Guid HotelId { get; set; }
    public Hotel? Hotel { get; set; }
    public Guid RoomTypeId { get; set; }
    public RoomType? RoomType { get; set; }

    public IEnumerable<Reservation>? Reservations { get; set; }
}
