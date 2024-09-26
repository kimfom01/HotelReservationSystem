using ReservationService.Application.Dtos.GuestProfiles;

namespace ReservationService.Application.Dtos.Reservations;

public record CreateReservationRequest(
    DateTime CheckIn,
    DateTime CheckOut,
    string SpecialRequests,
    string RoomPreferences,
    int NumberOfGuests,
    CreateGuestProfileRequest GuestProfile,
    Guid RoomTypeId,
    Guid HotelId);