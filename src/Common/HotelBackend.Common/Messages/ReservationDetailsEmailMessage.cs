namespace HotelBackend.Common.Messages;

public class ReservationDetailsEmailMessage
{
    public ReservationMessage? ReservationMessage { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string ReceiverEmail { get; set; } = string.Empty;
}