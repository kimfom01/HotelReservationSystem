using Hrs.Common.Messages;

namespace Hrs.Application.Contracts.Email;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string? subject, ReservationDetails? reservationMessage);
}