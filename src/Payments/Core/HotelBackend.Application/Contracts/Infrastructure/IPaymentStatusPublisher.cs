namespace HotelBackend.Application.Contracts.Infrastructure;

public interface IPaymentStatusPublisher : IDisposable
{
    Task PublishMessage<T>(T message);
}