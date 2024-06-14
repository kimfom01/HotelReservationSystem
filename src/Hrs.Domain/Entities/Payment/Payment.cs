using Hrs.Common.Enums;
using Hrs.Domain.Entities.Common;

namespace Hrs.Domain.Entities.Payment;

public class Payment : BaseEntity
{
    public DateTime PaidAt { get; set; } = DateTime.Now;
    public decimal Amount { get; set; }
    public Guid ReservationId { get; set; }
    public PaymentStatus Status { get; set; }
}