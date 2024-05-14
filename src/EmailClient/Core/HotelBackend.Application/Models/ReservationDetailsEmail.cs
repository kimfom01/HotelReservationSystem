using HotelBackend.Application.Dtos.Reservations;

namespace HotelBackend.Application.Models;

public class ReservationDetailsEmail
{
    public GetReservationDetailsDto? ReservationDetailsDto { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string ReceiverEmail { get; set; } = string.Empty;
}