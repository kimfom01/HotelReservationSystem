namespace Hrs.Application.Contracts.Email;

public interface IEmailSender
{
    Task SendEmailAsync<TBody>(EmailMessage message, TBody body, CancellationToken cancellationToken = default);
}