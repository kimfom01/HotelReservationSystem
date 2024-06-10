namespace HotelBackend.Common.Messages;

public record ReservationCreatedMessage
{
    public ReservationDetails? ReservationMessage { get; init; }
    public string Subject { get; init; } = string.Empty;
    public string ReceiverEmail { get; init; } = string.Empty;
}