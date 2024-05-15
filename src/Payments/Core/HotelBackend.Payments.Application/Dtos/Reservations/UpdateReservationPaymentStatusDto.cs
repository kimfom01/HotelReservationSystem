using HotelBackend.Common.Enums;

namespace HotelBackend.Payments.Application.Dtos.Reservations;

public class UpdateReservationPaymentStatusDto
{
    public PaymentStatus Status { get; set; }
    public Guid ReservationId { get; set; }
}