namespace HotelBackend.Payments.Application.Contracts.Infrastructure.MessageBroker;

public interface IPaymentStatusPublisher : IDisposable
{
    Task PublishMessage<T>(T message);
}