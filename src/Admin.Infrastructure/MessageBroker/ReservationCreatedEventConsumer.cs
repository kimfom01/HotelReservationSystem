using System.Text;
using Admin.Application.Contracts.Email;
using Admin.Application.Contracts.MessageBroker;
using Hrs.Common.Exceptions;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Admin.Infrastructure.MessageBroker;

public class ReservationCreatedEventConsumer : IConsumer<ReservationCreatedEvent>
{
    private readonly IEmailSender _emailSender;
    private readonly ILogger<ReservationCreatedEventConsumer> _logger;

    public ReservationCreatedEventConsumer(IEmailSender emailSender, ILogger<ReservationCreatedEventConsumer> logger)
    {
        _emailSender = emailSender;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ReservationCreatedEvent> context)
    {
        _logger.LogInformation("Consuming {EventName} event", nameof(ReservationCreatedEvent));

        try
        {
            var emailTemplate = new StringBuilder()
                .Append("<h3>Hello, @Model.ReceiverName</h3>")
                .Append("<div>Your reservation was successfully created</div>")
                .Append("<div>Please complete your payment</div>")
                .Append("<div>Regards, Hotel Reservation Team</div>")
                .ToString();

            var emailModel = new EmailMessage
            {
                Subject = context.Message.Subject,
                Template = emailTemplate,
                ReceiverEmail = context.Message.ReceiverEmail,
                ReceiverName = context.Message.ReceiverName
            };

            await _emailSender.SendEmailAsync(emailModel, new { emailModel.ReceiverName }, context.CancellationToken);
        }
        catch (SendFailException ex)
        {
            _logger.LogError(ex, "An error occured: {Error}", ex.Message);
        }
    }
}