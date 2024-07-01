using System.Text;
using FluentEmail.Core;
using Hrs.Application.Contracts.Email;
using Hrs.Application.Exceptions;
using Hrs.Common.Enums;
using Hrs.Common.Messages;
using Microsoft.Extensions.Logging;

namespace Hrs.Infrastructure.Email;

public class EmailSender : IEmailSender
{
    private readonly ILogger<EmailSender> _logger;
    private readonly IFluentEmail _fluentEmail;

    public EmailSender(ILogger<EmailSender> logger, IFluentEmail fluentEmail)
    {
        _logger = logger;
        _fluentEmail = fluentEmail;
    }

    public async Task SendEmailAsync(string email, string? subject, ReservationDetails? reservationMessage)
    {
        // TODO: Use a razor template to build this message
        
        var htmlMessageStringbuilder = new StringBuilder()
            .Append("<h3>Hello, @Model.GuestFirstName</h3>");

        if (reservationMessage is null)
        {
            _logger.LogError("Reservation object is empty");
            return;
        }

        switch (reservationMessage.PaymentStatus)
        {
            case PaymentStatus.Paid:
                htmlMessageStringbuilder
                    .Append(
                        "<div>Your reservation on @Model.HotelName, @Model.HotelLocation was successfully paid</div>");
                break;
            case PaymentStatus.Canceled:
                htmlMessageStringbuilder
                    .Append(
                        "<div>Your reservation on @Model.HotelName, @Model.HotelLocation was cancelled</div>");
                break;
            case PaymentStatus.Pending:
                break;
            case PaymentStatus.Refunded:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        htmlMessageStringbuilder
            .Append("<div>Details:</div>")
            .Append("<div>Check In: @Model.CheckIn.ToString(\"dd MMM HH:mm\")</div>")
            .Append("<div>Check Out: @Model.CheckOut.ToString(\"dd MMM HH:mm\")</div>")
            .Append(
                "<div>Status: ");

        switch (reservationMessage.ReservationStatus)
        {
            case ReservationStatus.Pending:
                htmlMessageStringbuilder.Append("<span>PENDING</span>");
                break;
            case ReservationStatus.Confirmed:
                htmlMessageStringbuilder.Append("<span>CONFIRMED</span>:");
                break;
            case ReservationStatus.Cancelled:
                htmlMessageStringbuilder.Append("<span>CANCELLED</span>");
                break;
        }

        var htmlMessage = htmlMessageStringbuilder.Append("</div>")
            .ToString();

        _logger.LogInformation("Sending email");

        var sendResponse = await _fluentEmail
            .To(email)
            .Subject(subject)
            .UsingTemplate(htmlMessage, reservationMessage)
            .SendAsync();

        if (!sendResponse.Successful)
        {
            _logger.LogError("Error occured while sending email: {Errors}", sendResponse.ErrorMessages);
            throw new SendFailException("Failed to send email:");
        }

        _logger.LogInformation("Email successfully sent");
    }
}