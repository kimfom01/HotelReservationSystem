namespace Admin.Application.Contracts.MessageBroker;

public record ReservationCreatedEvent
{
    public string Subject { get; init; } = string.Empty;
    public string ReceiverEmail { get; init; } = string.Empty;
    public string? ReceiverName { get; init; } = string.Empty;
}