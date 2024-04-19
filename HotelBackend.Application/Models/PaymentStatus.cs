using HotelBackend.Domain.Enums;

namespace HotelBackend.Application.Models;

public class PaymentStatus
{
    public PaymentStatusEnum Status { get; set; }
    public Guid ReservationId { get; set; }
}