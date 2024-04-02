using System.ComponentModel.DataAnnotations;
using HotelBackend.ReservationService.Enums;
using HotelBackend.ReservationService.Guest;
using HotelBackend.ReservationService.Hotel;
using HotelBackend.ReservationService.Room;

namespace HotelBackend.ReservationService.Reservation;

public class ReservationModel
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
    public GuestProfile GuestProfile { get; set; }
    public Guid HotelId { get; set; }
    public HotelModel? Hotel { get; set; }
    public Guid RoomId { get; set; }

    public RoomModel? Room { get; set; }
    // public decimal Tax { get; set; }
    // public double Discount { get; set; }
}