using HotelBackend.Application.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace HotelBackend.Infrastructure.RabbitMqService;

public abstract class RabbitMqClientBase : IDisposable
{
    private readonly RabbitMqOption _rabbitMqOptions;
    private readonly IConnection _connection;
    protected readonly IModel Channel;

    protected string ExchangeName { get; private set; } = string.Empty;
    protected string RoutingKey { get; private set; } = string.Empty;
    public string QueueName { get; private set; } = string.Empty;

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
        Channel.BasicQos(0, 1, false);
    }

    public void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        _connection.Dispose();
        Channel.Dispose();
    }
}