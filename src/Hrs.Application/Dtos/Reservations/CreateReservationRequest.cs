using Hrs.Application.Dtos.GuestProfiles;

namespace Hrs.Application.Dtos.Reservations;

public record CreateReservationRequest
{
    public DateTime CheckIn { get; init; }
    public DateTime CheckOut { get; init; }
    public string SpecialRequests { get; init; } = string.Empty;
    public string RoomPreferences { get; init; } = string.Empty;
    public int NumberOfGuests { get; init; }
    public CreateGuestProfileRequest? GuestProfile { get; init; }
    public Guid RoomTypeId { get; init; }
    public Guid HotelId { get; init; }
}
