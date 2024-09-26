using Admin.Application.Contracts.MessageBroker;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Admin.Infrastructure.MessageBroker;

public class ReservationPaymentStatusUpdatedEventHandler : IConsumer<ReservationPaymentStatusUpdatedEvent>
{
    private readonly ILogger<ReservationPaymentStatusUpdatedEventHandler> _logger;

    public ReservationPaymentStatusUpdatedEventHandler(ILogger<ReservationPaymentStatusUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public  Task Consume(ConsumeContext<ReservationPaymentStatusUpdatedEvent> context)
    {
        _logger.LogInformation("Consuming {EventName} event", nameof(ReservationPaymentStatusUpdatedEvent));
        
        return Task.CompletedTask;
    }
}