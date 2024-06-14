using HotelBackend.Common.Enums;
using HotelBackend.Reservations.Application.Dtos.Common;
using HotelBackend.Reservations.Application.Dtos.GuestProfiles;

namespace HotelBackend.Reservations.Application.Dtos.Reservations;

public record GetReservationDetailsResponse : BaseDto
{
    public DateTime CreatedAt { get; init; }
    public DateTime CheckIn { get; init; }
    public DateTime CheckOut { get; init; }
    public ReservationStatus ReservationStatus { get; init; }
    public PaymentStatus PaymentStatus { get; init; }
    public string? PaymentId { get; init; }
    public string? SpecialRequests { get; init; }
    public string? RoomPreferences { get; init; }
    public int NumberOfGuests { get; init; }
    public Guid GuestProfileId { get; init; }
    public GetGuestProfileResponse? GuestProfile { get; init; } = new();
    public Guid HotelId { get; init; }
    public Guid RoomId { get; init; }
}