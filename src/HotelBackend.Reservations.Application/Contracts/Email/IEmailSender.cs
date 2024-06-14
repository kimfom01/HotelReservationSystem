using HotelBackend.Common.Messages;

namespace HotelBackend.Reservations.Application.Contracts.Email;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string? subject, ReservationDetails? reservationMessage);
}