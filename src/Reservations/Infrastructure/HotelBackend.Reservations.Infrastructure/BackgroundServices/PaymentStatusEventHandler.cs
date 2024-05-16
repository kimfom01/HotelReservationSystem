using HotelBackend.Reservations.Application.Contracts.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Reservations.Infrastructure.BackgroundServices;

public class PaymentStatusEventHandler : BackgroundService
{
    private readonly IEmailQueueSubscriber _queueSubscriber;

    public PaymentStatusEventHandler(
        ILogger<PaymentStatusEventHandler> logger,
        IEmailQueueSubscriber queueSubscriber
    )
    {
        _queueSubscriber = queueSubscriber;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!stoppingToken.IsCancellationRequested)
        {
            await _queueSubscriber.SubscribeToQueue(stoppingToken);
        }
    }
}