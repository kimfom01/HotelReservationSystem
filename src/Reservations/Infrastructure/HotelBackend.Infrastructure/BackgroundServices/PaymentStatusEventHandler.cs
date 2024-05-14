using HotelBackend.Application.Contracts.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Infrastructure.BackgroundServices;

public class PaymentStatusEventHandler : BackgroundService
{
    private readonly ILogger<PaymentStatusEventHandler> _logger;
    private readonly IEmailQueueSubscriber _queueSubscriber;

    public PaymentStatusEventHandler(
        ILogger<PaymentStatusEventHandler> logger,
        IEmailQueueSubscriber queueSubscriber
    )
    {
        _logger = logger;
        _queueSubscriber = queueSubscriber;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!stoppingToken.IsCancellationRequested)
        {
            _queueSubscriber.SubcribeToQueue(stoppingToken);
        }

        return Task.CompletedTask;
    }
}
