using System.Text;
using FluentEmail.Core;
using HotelBackend.Application.Contracts.Infrastructure;
using HotelBackend.Application.Dtos.Reservations;
using HotelBackend.Application.Exceptions;
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
        var htmlMessage = new StringBuilder()
            .Append("<h3>Hello, @Model.GuestProfile.FirstName</h3>")
            .Append("<div>Your reservation on @Model.Hotel.Name, @Model.Hotel.Location was successful</div>")
            .Append("<div>Details:</div>")
            .Append("<div>Check In: @Model.CheckIn.ToString(\"dd MMM HH:mm\")</div>")
            .Append("<div>Check Out: @Model.CheckOut.ToString(\"dd MMM HH:mm\")</div>")
            .Append("<div>Status: @if(Model.ReservationStatus == 0) { <span>PENDING</span> } else if(Model.ReservationStatus == 1 ){ <span>CONFIRMED</span> } else {<span>CANCELLED</span>}</div>")
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