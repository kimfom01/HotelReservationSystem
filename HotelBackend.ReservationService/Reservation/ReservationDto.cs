using HotelBackend.ReservationService.Dtos;
using HotelBackend.ReservationService.Guest;

namespace HotelBackend.ReservationService.Reservation;

public class ReservationDto
{
    public Guid Id { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public string SpecialRequests { get; set; }
    public string RoomPreferences { get; set; }
    public int NumberOfGuests { get; set; }
    public Guid GuestProfileId { get; set; }
    public GuestProfileDto GuestProfile { get; set; }
    public Guid HotelId { get; set; }
    public Guid RoomId { get; set; }
    public RoomDto? Room { get; set; }
}
