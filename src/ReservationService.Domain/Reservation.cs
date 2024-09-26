using System.ComponentModel.DataAnnotations;
using Hrs.Common.Entities;
using Hrs.Common.Enums;

namespace ReservationService.Domain;

public class Reservation : BaseEntity
{
    internal Reservation(
        Guid id,
        DateTime checkIn,
        DateTime checkOut,
        string? specialRequests,
        string? roomPreferences,
        int numberOfGuests,
        Guid guestProfileId,
        Guid roomId,
        Guid hotelId)
    {
        Id = id;
        CheckIn = checkIn;
        CheckOut = checkOut;
        SpecialRequests = specialRequests;
        RoomPreferences = roomPreferences;
        NumberOfGuests = numberOfGuests;
        GuestProfileId = guestProfileId;
        RoomId = roomId;
        HotelId = hotelId;
    }

    public DateTime CheckIn { get; private set; }
    public DateTime CheckOut { get; private set; }
    public ReservationStatus ReservationStatus { get; private set; } = ReservationStatus.Pending;
    public PaymentStatus PaymentStatus { get; private set; } = PaymentStatus.Pending;
    [MaxLength(200)] public Guid? PaymentId { get; private set; }
    [MaxLength(500)] public string? SpecialRequests { get; private set; }
    [MaxLength(500)] public string? RoomPreferences { get; private set; }
    public int NumberOfGuests { get; private set; }
    public Guid GuestProfileId { get; private set; }
    public GuestProfile? GuestProfile { get; private set; }
    public Guid RoomId { get; private set; }
    public Guid HotelId { get; private set; }

    public static Reservation CreateReservation(
        DateTime checkIn,
        DateTime checkOut,
        string? specialRequests,
        string? roomPreferences,
        int numberOfGuests,
        Guid guestProfileId,
        Guid roomId,
        Guid hotelId)
    {
        return new Reservation(
            Guid.NewGuid(),
            checkIn,
            checkOut,
            specialRequests,
            roomPreferences,
            numberOfGuests,
            guestProfileId,
            roomId,
            hotelId);
    }

    public void UpdateStatuses(PaymentStatus paymentStatus, Guid paymentId)
    {
        PaymentStatus = paymentStatus;
        PaymentId = paymentId;
        
        if (paymentStatus == PaymentStatus.Paid)
        {
            ReservationStatus = ReservationStatus.Confirmed;
        }
    }

    public void CancelReservation()
    {
        ReservationStatus = ReservationStatus.Cancelled;
    }
}