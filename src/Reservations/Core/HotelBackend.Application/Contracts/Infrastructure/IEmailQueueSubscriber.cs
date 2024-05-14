namespace HotelBackend.Application.Contracts.Infrastructure;

public interface IEmailQueueSubscriber
{
    Task SubcribeToQueue(CancellationToken stoppingToken);
}