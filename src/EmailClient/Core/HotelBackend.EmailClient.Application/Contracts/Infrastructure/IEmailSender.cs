using HotelBackend.Common.Models;

namespace HotelBackend.EmailClient.Application.Contracts.Infrastructure;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string? subject, ReservationMessage reservationMessage);
}