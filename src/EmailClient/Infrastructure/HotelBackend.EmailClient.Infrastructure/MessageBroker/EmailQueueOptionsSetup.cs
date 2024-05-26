using HotelBackend.Common.Models.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace HotelBackend.EmailClient.Infrastructure.MessageBroker;

public class EmailQueueOptionsSetup : IConfigureOptions<EmailQueueOptions>
{
    private readonly IConfiguration _configuration;

    public EmailQueueOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(EmailQueueOptions options)
    {
        _configuration.GetSection(nameof(EmailQueueOptions)).Bind(options);
    }
}