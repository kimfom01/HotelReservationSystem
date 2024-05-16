using HotelBackend.Reservations.Application.Dtos.GuestProfiles;

namespace HotelBackend.Reservations.Application.Dtos.Reservations;

public class CreateReservationDto
{
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public string SpecialRequests { get; set; } = string.Empty;
    public string RoomPreferences { get; set; } = string.Empty;
    public int NumberOfGuests { get; set; }
    public CreateGuestProfileDto? GuestProfile { get; set; }
    public Guid RoomId { get; set; }
}
