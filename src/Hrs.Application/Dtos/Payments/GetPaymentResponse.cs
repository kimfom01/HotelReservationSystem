using Hrs.Common.Enums;

namespace Hrs.Application.Dtos.Payments;

public record GetPaymentResponse
{
    public Guid Id { get; init; }
    public DateTime PaidAt { get; init; }
    public decimal Amount { get; init; }
    public Guid ReservationId { get; init; }
    public PaymentStatus Status { get; init; }
}