using FluentEmail.Core;
using Hrs.Application.Contracts.Email;
using Hrs.Common.Exceptions;
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

    public async Task SendEmailAsync<TBody>(EmailMessage message, TBody body, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Sending email");
        
        var sendResponse = await _fluentEmail
            .To(message.ReceiverEmail, message.ReceiverName)
            .Subject(message.Subject)
            .UsingTemplate(message.Template, body)
            .SendAsync(cancellationToken);

        if (!sendResponse.Successful)
        {
            _logger.LogError("Error occured while sending email: {Errors}", sendResponse.ErrorMessages);
            throw new SendFailException("Failed to send email");
        }

        _logger.LogInformation("Email successfully sent");
    }
}