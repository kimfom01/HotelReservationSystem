using HotelBackend.Application.Dtos.Reservations;

namespace HotelBackend.Application.Contracts.Infrastructure;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string? subject, GetReservationDetailsDto reservationDetails);
}