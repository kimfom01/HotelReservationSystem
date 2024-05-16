using HotelBackend.Common.Enums;
using HotelBackend.Payments.Domain.Entities.Common;

namespace HotelBackend.Payments.Domain.Entities;

public class Payment : BaseEntity
{
    public DateTime PaidAt { get; set; } = DateTime.Now;
    public decimal Amount { get; set; }
    public Guid ReservationId { get; set; }
    public PaymentStatus Status { get; set; }
}