using HotelBackend.Common.Enums;

namespace HotelBackend.Reservations.Application.Dtos.Reservations;

public class UpdateReservationPaymentStatusDto
{
    public Guid PaymentId { get; set; }
    public PaymentStatus Status { get; set; }
    public Guid ReservationId { get; set; }
}