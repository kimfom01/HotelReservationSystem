using HotelBackend.Common.Messages;
using MassTransit;

namespace HotelBackend.Reservations.Application.Contracts.Infrastructure.MessageBroker;

public interface IPaymentQueueConsumer: IConsumer<PaymentStatusMessage>
{
    // Task SubscribeToQueue(CancellationToken stoppingToken);
}