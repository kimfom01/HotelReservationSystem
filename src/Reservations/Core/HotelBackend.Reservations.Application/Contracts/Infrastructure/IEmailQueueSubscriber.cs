namespace HotelBackend.Reservations.Application.Contracts.Infrastructure;

public interface IEmailQueueSubscriber
{
    Task SubscribeToQueue(CancellationToken stoppingToken);
}