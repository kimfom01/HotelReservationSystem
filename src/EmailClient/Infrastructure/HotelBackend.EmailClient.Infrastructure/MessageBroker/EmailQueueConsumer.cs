using HotelBackend.Common.Messages;
using HotelBackend.EmailClient.Application.Contracts.Infrastructure;
using HotelBackend.EmailClient.Application.Contracts.MessageBroker;
using HotelBackend.EmailClient.Application.Exceptions;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HotelBackend.EmailClient.Infrastructure.MessageBroker;

public class EmailQueueConsumer : IEmailQueueConsumer
{
    private readonly IEmailSender _emailSender;
    private readonly ILogger<EmailQueueConsumer> _logger;

    public EmailQueueConsumer(IEmailSender emailSender, ILogger<EmailQueueConsumer> logger)
    {
        _emailSender = emailSender;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ReservationDetailsEmailMessage> context)
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