using Hrs.Application.Dtos.GuestProfiles;

namespace Hrs.Application.Dtos.Reservations;

public record CreateReservationRequest(
    DateTime CheckIn,
    DateTime CheckOut,
    string SpecialRequests,
    string RoomPreferences,
    int NumberOfGuests,
    CreateGuestProfileRequest GuestProfile,
    Guid RoomTypeId,
    Guid HotelId);