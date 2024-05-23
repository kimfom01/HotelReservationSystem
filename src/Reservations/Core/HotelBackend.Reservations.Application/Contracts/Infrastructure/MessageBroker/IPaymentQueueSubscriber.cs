namespace HotelBackend.Reservations.Application.Contracts.Infrastructure.MessageBroker;

public interface IPaymentQueueSubscriber
{
    Task SubscribeToQueue(CancellationToken stoppingToken);
}