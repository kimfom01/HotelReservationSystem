using HotelBackend.Common.Enums;

namespace HotelBackend.Payments.Application.Dtos.Payments;

public record AddPaymentRequest
{
    public decimal Amount { get; init; }
    public Guid ReservationId { get; init; }
    public PaymentStatus Status { get; init; }
}