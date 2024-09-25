using Hrs.Common.Entities;
using Hrs.Common.Enums;

namespace Hrs.Domain.Entities.Payment;

public class Payment : BaseEntity
{
    internal Payment(decimal amount, Guid reservationId, PaymentStatus status)
    {
        Id = Guid.NewGuid();
        PaidAt = DateTime.Now;
        Amount = amount;
        ReservationId = reservationId;
        Status = status;
    }

    public DateTime PaidAt { get; private set; }
    public decimal Amount { get; private set; }
    public Guid ReservationId { get; private set; }
    public PaymentStatus Status { get; private set; }

    public static Payment CreatePayment(decimal amount, Guid reservationId, PaymentStatus status)
    {
        return new Payment(amount, reservationId, status);
    }
}