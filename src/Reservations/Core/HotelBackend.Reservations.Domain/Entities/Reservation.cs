using System.ComponentModel.DataAnnotations;
using HotelBackend.Common.Enums;

namespace HotelBackend.Reservations.Domain.Entities;

public class Reservation
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; private set; } = DateTime.Now;
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public ReservationStatus ReservationStatus { get; set; } = ReservationStatus.PENDING;
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.PENDING;
    [MaxLength(200)] public string? PaymentId { get; set; }
    [MaxLength(500)] public string? SpecialRequests { get; set; }
    [MaxLength(500)] public string? RoomPreferences { get; set; }
    public int NumberOfGuests { get; set; }
    public Guid GuestProfileId { get; set; }
    public GuestProfile? GuestProfile { get; set; }
    public Guid HotelId { get; set; }
    public Hotel? Hotel { get; set; }
    public Guid RoomId { get; set; }

    public Room? Room { get; set; }
}