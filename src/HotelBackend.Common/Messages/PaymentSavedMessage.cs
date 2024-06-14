using HotelBackend.Common.Enums;

namespace HotelBackend.Common.Messages;

public record PaymentSavedMessage
{
    public Guid Id { get; init; }
    public DateTime PaidAt { get; init; }
    public decimal Amount { get; init; }
    public Guid ReservationId { get; init; }
    public PaymentStatus Status { get; init; }
}