using HotelBackend.Common.Enums;
using HotelBackend.Reservations.Domain.Entities.Common;

namespace HotelBackend.Reservations.Domain.Entities.Payment;

public class Payment : BaseEntity
{
    public DateTime PaidAt { get; set; } = DateTime.Now;
    public decimal Amount { get; set; }
    public Guid ReservationId { get; set; }
    public PaymentStatus Status { get; set; }
}