using HotelBackend.Common.Models.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace HotelBackend.Reservations.Infrastructure.MessageBroker;

public class PaymentQueueOptionsSetup : IConfigureOptions<PaymentQueueOptions>
{
    private readonly IConfiguration _configuration;

    public PaymentQueueOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(PaymentQueueOptions options)
    {
        _configuration.GetSection(nameof(PaymentQueueOptions)).Bind(options);
    }
}