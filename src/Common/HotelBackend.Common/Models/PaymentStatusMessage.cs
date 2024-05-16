using HotelBackend.Common.Enums;

namespace HotelBackend.Common.Models;

public class PaymentStatusMessage
{
    public Guid Id { get; set; }
    public DateTime PaidAt { get; set; }
    public decimal Amount { get; set; }
    public Guid ReservationId { get; set; }
    public PaymentStatus Status { get; set; }
}