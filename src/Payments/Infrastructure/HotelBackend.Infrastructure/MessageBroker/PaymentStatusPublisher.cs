using System.Text;
using System.Text.Json;
using HotelBackend.Application.Contracts.Infrastructure;
using HotelBackend.Common.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace HotelBackend.Infrastructure.MessageBroker;

public class PaymentStatusPublisher : IPaymentStatusPublisher
{
    private readonly ILogger<PaymentStatusPublisher> _logger;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    private readonly string _exchangeName;
    private readonly string _routingKey;
    private readonly string _queueName;

    public PaymentStatusPublisher(
        IOptions<Config> configOptions,
        IConnectionFactory factory,
        ILogger<PaymentStatusPublisher> logger)
    {
        _logger = logger;
        var rabbitMqOptions = configOptions.Value.PaymentQueueOption;
        factory.Uri = new Uri(
            $"amqp://{rabbitMqOptions!.User}:{rabbitMqOptions.Password}@{rabbitMqOptions.Host}:{rabbitMqOptions.Port}");
        factory.ClientProvidedName = rabbitMqOptions.ClientName;
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _exchangeName = rabbitMqOptions.Exchange;
        _routingKey = rabbitMqOptions.RoutingKey;
        _queueName = rabbitMqOptions.QueueName;

        _channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct);
        _channel.QueueDeclare(_queueName, false, false, false, null);
        _channel.QueueBind(_queueName, _exchangeName, _routingKey, null);
    }

    public Task PublishMessage<T>(T message)
    {
        var serializedMessage = JsonSerializer.Serialize(message);
        var messageBodyBytes = Encoding.UTF8.GetBytes(serializedMessage);

        _logger.LogInformation("Publishing message to {queueName}", _queueName);
        _channel.BasicPublish(_exchangeName, _routingKey, null, messageBodyBytes);
        _logger.LogInformation("Published message to {queueName}", _queueName);
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