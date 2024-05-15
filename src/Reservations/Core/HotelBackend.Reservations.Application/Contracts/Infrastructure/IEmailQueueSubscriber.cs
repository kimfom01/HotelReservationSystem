namespace HotelBackend.Reservations.Application.Contracts.Infrastructure;

public interface IEmailQueueSubscriber
{
    Task SubcribeToQueue(CancellationToken stoppingToken);
}