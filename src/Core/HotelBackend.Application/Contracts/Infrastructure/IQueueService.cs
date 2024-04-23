namespace HotelBackend.Application.Contracts.Infrastructure;

public interface IQueueService
{
    Task PublishMessage<T>(T message);
}