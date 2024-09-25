using Hrs.Common.Enums;

namespace ReservationService.Application.Contracts.MessageBroker;

public record PaymentSavedEvent(
    Guid Id,
    DateTime PaidAt,
    decimal Amount,
    Guid ReservationId,
    PaymentStatus Status
);