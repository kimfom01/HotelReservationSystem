namespace HotelBackend.Reservations.Application.Contracts.MessageBroker;

public interface IEmailQueuePublisher: IDisposable
{
    Task PublishMessage<T>(T message);
}