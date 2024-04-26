namespace HotelBackend.Application.Contracts.Infrastructure;

public interface IReservationQueueService : IDisposable
{
    Task PublishMessage<T>(T message);
}