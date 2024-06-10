using HotelBackend.Common.Messages;

namespace HotelBackend.EmailClient.Application.Contracts.Infrastructure;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string? subject, ReservationDetails? reservationMessage);
}