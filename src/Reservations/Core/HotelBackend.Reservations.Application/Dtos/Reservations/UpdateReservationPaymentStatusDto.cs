using HotelBackend.Common.Enums;

namespace HotelBackend.Reservations.Application.Dtos.Reservations;

public class UpdateReservationPaymentStatusDto
{
    public PaymentStatus Status { get; set; }
    public Guid ReservationId { get; set; }
}