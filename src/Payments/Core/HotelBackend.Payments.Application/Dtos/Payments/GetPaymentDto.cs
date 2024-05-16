using HotelBackend.Common.Enums;

namespace HotelBackend.Payments.Application.Dtos.Payments;

public class GetPaymentDto
{
    public Guid Id { get; set; }
    public DateTime PaidAt { get; set; }
    public decimal Amount { get; set; }
    public Guid ReservationId { get; set; }
    public PaymentStatus Status { get; set; }
}