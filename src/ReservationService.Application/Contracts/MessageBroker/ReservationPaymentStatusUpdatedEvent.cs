using Hrs.Common.Enums;

namespace ReservationService.Application.Contracts.MessageBroker;

public class ReservationPaymentStatusUpdatedEvent
{
    public string Subject { get; init; } = string.Empty;
    public string ReceiverEmail { get; init; } = string.Empty;
    public string? ReceiverName { get; init; } = string.Empty;
    public ReservationStatus ReservationStatus { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}