using Hrs.Common.Enums;

namespace Hrs.Common.Messages;

public record PaymentSavedMessage(
    Guid Id,
    DateTime PaidAt,
    decimal Amount,
    Guid ReservationId,
    PaymentStatus Status
);