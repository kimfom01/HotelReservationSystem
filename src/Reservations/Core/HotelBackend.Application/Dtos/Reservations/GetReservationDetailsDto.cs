using HotelBackend.Application.Dtos.Common;
using HotelBackend.Application.Dtos.GuestProfiles;
using HotelBackend.Common.Enums;

namespace HotelBackend.Application.Dtos.Reservations;

public class GetReservationDetailsDto : BaseDto
{
    public DateTime CreationDate { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public ReservationStatus ReservationStatus { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public string? PaymentId { get; set; }
    public string? SpecialRequests { get; set; }
    public string? RoomPreferences { get; set; }
    public int NumberOfGuests { get; set; }
    public Guid GuestProfileId { get; set; }
    public GetGuestProfileDto? GuestProfile { get; set; } = new();
    public Guid HotelId { get; set; }
    public HotelDto? Hotel { get; set; } = new();
    public Guid RoomId { get; set; }
    public RoomDto? Room { get; set; } = new();
}