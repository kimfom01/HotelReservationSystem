namespace HotelBackend.Payments.Application.Contracts.Infrastructure;

public interface IPaymentStatusPublisher : IDisposable
{
    Task PublishMessage<T>(T message);
}