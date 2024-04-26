namespace HotelBackend.Application.Contracts.Infrastructure;

public interface IEmailQueueService: IDisposable
{
    Task PublishMessage<T>(T message);
}