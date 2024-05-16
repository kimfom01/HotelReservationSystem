using HotelBackend.Common.Enums;

namespace HotelBackend.Payments.Application.Dtos.Payments;

public class AddPaymentDto
{
    public decimal Amount { get; set; }
    public Guid ReservationId { get; set; }
    public PaymentStatus Status { get; set; }
}