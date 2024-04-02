using HotelBackend.ReservationService.Utils;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace HotelBackend.ReservationService.Infrastructure;

public class RabbitMqClientBase : IDisposable
{
    private readonly RabbitMqOption _rabbitMqOptions;
    private readonly IConnection _connection;
    protected readonly IModel Channel;

    protected string ExchangeName { get; private set; }
    protected string RoutingKey { get; private set; }
    private string QueueName { get; set; }

    protected RabbitMqClientBase(
        IOptions<RabbitMqOption> rabbitMqOptions,
        IConnectionFactory factory)
    {
        _rabbitMqOptions = rabbitMqOptions.Value;
        factory.Uri =
            new Uri(
                $"amqp://{_rabbitMqOptions.User}:{_rabbitMqOptions.Password}@{_rabbitMqOptions.Host}:{_rabbitMqOptions.Port}");
        factory.ClientProvidedName = _rabbitMqOptions.ClientName;
        _connection = factory.CreateConnection();
        Channel = _connection.CreateModel();
        ConnectToRabbitMq();
    }

    private void ConnectToRabbitMq()
    {
        ExchangeName = _rabbitMqOptions.Exchange;
        RoutingKey = _rabbitMqOptions.RoutingKey;
        QueueName = _rabbitMqOptions.QueueName;
        Channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct);
        Channel.QueueDeclare(QueueName, false, false, false, null);
        Channel.QueueBind(QueueName, ExchangeName, RoutingKey, null);
    }

    public void Dispose()
    {
        _connection.Dispose();
        Channel.Dispose();
    }
}