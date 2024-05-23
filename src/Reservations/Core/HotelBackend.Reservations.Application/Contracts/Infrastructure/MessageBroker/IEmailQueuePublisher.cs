namespace HotelBackend.Reservations.Application.Contracts.Infrastructure.MessageBroker;

public interface IEmailQueuePublisher: IDisposable
{
    Task PublishMessage<T>(T message);
}