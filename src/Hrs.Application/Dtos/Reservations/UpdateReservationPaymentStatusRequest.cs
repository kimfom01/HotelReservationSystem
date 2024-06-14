using Hrs.Common.Enums;

namespace Hrs.Application.Dtos.Reservations;

public record UpdateReservationPaymentStatusRequest
{
    public Guid PaymentId { get; init; }
    public PaymentStatus Status { get; init; }
    public Guid ReservationId { get; init; }
}