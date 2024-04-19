using HotelBackend.Application.Dtos.Common;
using HotelBackend.Application.Dtos.GuestProfiles;
using HotelBackend.Domain.Enums;

namespace HotelBackend.Application.Dtos.Reservations;

public class GetReservationDto : BaseDto
{
    public DateTime CreationDate { get; private set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public ReservationStatus ReservationStatus { get; set; }
    public PaymentStatusEnum PaymentStatusEnum { get; set; }
    public string? PaymentId { get; set; }
    public string? SpecialRequests { get; set; }
    public string? RoomPreferences { get; set; }
    public int NumberOfGuests { get; set; }
    public Guid GuestProfileId { get; set; }
    public CreateGuestProfileDto? GuestProfile { get; set; }
    public Guid HotelId { get; set; }
    public HotelDto? Hotel { get; set; }
    public Guid RoomId { get; set; }
    public RoomDto? Room { get; set; }
}