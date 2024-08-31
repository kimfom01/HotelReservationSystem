using Hrs.Application.Contracts.Email;
using Hrs.Application.Exceptions;
using Hrs.Common.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Hrs.Infrastructure.MessageBroker;

public class EmailQueueConsumer : IConsumer<ReservationCreatedMessage>
{
    private readonly IEmailSender _emailSender;
    private readonly ILogger<EmailQueueConsumer> _logger;

    public EmailQueueConsumer(IEmailSender emailSender, ILogger<EmailQueueConsumer> logger)
    {
        _emailSender = emailSender;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ReservationCreatedMessage> context)
    {
        try
        {
            await _emailSender.SendEmailAsync(context.Message.ReceiverEmail, context.Message.Subject,
                context.Message.ReservationMessage);
        }
        catch (SendFailException ex)
        {
            _logger.LogError(ex, "An error occured: {Error}", ex.Message);
        }
    }
}