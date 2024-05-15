namespace HotelBackend.Reservations.Application.Contracts.Infrastructure;

public interface IEmailQueuePublisher: IDisposable
{
    Task PublishMessage<T>(T message);
}