using System.Text;
using System.Text.Json;
using HotelBackend.Common.Models.Options;
using HotelBackend.Payments.Application.Contracts.Infrastructure.MessageBroker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace HotelBackend.Payments.Infrastructure.MessageBroker;

public class PaymentStatusPublisher : IPaymentStatusPublisher
{
    private readonly ILogger<PaymentStatusPublisher> _logger;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    private readonly string _exchangeName;
    private readonly string _routingKey;
    private readonly string _queueName;

    public PaymentStatusPublisher(
        IOptions<PaymentQueueOptions> configOptions,
        IConnectionFactory factory,
        ILogger<PaymentStatusPublisher> logger)
    {
        _logger = logger;
        var queueOptions = configOptions.Value;
        factory.Uri = new Uri(
            $"amqp://{queueOptions.User}:{queueOptions.Password}@{queueOptions.Host}:{queueOptions.Port}");
        factory.ClientProvidedName = queueOptions.ClientName;
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _exchangeName = queueOptions.Exchange;
        _routingKey = queueOptions.RoutingKey;
        _queueName = queueOptions.QueueName;

        _channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct);
        _channel.QueueDeclare(_queueName, false, false, false, null);
        _channel.QueueBind(_queueName, _exchangeName, _routingKey, null);
    }

    public Task PublishMessage<T>(T message)
    {
        var serializedMessage = JsonSerializer.Serialize(message);
        var messageBodyBytes = Encoding.UTF8.GetBytes(serializedMessage);

        _logger.LogInformation("Publishing message to {QueueName}", _queueName);
        _channel.BasicPublish(_exchangeName, _routingKey, null, messageBodyBytes);
        _logger.LogInformation("Published message to {QueueName}", _queueName);
        return Task.CompletedTask;
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
        _channel.Dispose();
    }
}