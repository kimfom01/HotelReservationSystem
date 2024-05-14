using System.Text;
using FluentEmail.Core;
using HotelBackend.Application.Contracts.Infrastructure;
using HotelBackend.Application.Dtos.Reservations;
using HotelBackend.Application.Exceptions;
using HotelBackend.Common.Enums;
using HotelBackend.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Infrastructure.EmailProvider;

public class EmailSender : IEmailSender
{
    private readonly ILogger<EmailSender> _logger;
    private readonly IFluentEmail _fluentEmail;

    public EmailSender(ILogger<EmailSender> logger,
        IFluentEmail fluentEmail)
    {
        _logger = logger;
        _fluentEmail = fluentEmail;
    }

    public async Task SendEmailAsync(string email, string? subject, GetReservationDetailsDto reservationDetails)
    {
        var htmlMessageStringbuilder = new StringBuilder()
            .Append("<h3>Hello, @Model.GuestProfile.FirstName</h3>");

        switch (reservationDetails.PaymentStatus)
        {
            case PaymentStatus.PAID:
                htmlMessageStringbuilder
                    .Append(
                        "<div>Your reservation on @Model.Hotel.Name, @Model.Hotel.Location was successfully paid</div>");
                break;
            case PaymentStatus.CANCELED:
                htmlMessageStringbuilder
                    .Append(
                        "<div>Your reservation on @Model.Hotel.Name, @Model.Hotel.Location was cancelled</div>");
                break;
        }

        htmlMessageStringbuilder
            .Append("<div>Details:</div>")
            .Append("<div>Check In: @Model.CheckIn.ToString(\"dd MMM HH:mm\")</div>")
            .Append("<div>Check Out: @Model.CheckOut.ToString(\"dd MMM HH:mm\")</div>")
            .Append(
                "<div>Status: ");

        switch (reservationDetails.ReservationStatus)
        {
            case ReservationStatus.PENDING:
                htmlMessageStringbuilder.Append("<span>PENDING</span>");
                break;
            case ReservationStatus.CONFIRMED:
                htmlMessageStringbuilder.Append("<span>CONFIRMED</span>:");
                break;
            case ReservationStatus.CANCELLED:
                htmlMessageStringbuilder.Append("<span>CANCELLED</span>");
                break;
        }

        var htmlMessage = htmlMessageStringbuilder.Append("</div>")
            .ToString();

        _logger.LogInformation("Sending email");

        var sendResponse = await _fluentEmail
            .To(email)
            .Subject(subject)
            .UsingTemplate(htmlMessage, reservationDetails)
            .SendAsync();

        if (!sendResponse.Successful)
        {
            _logger.LogError("Error occured while sending email: {errors}", sendResponse.ErrorMessages);
            throw new SendFailException("Failed to send email:");
        }

        _logger.LogInformation("Email successfully sent");
    }
}