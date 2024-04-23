using System.Text;
using System.Text.Json;
using HotelBackend.Application.Contracts.Infrastructure;
using HotelBackend.Application.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace HotelBackend.Infrastructure.RabbitMqService;

public class QueueService : RabbitMqClientBase, IQueueService
{
    private readonly ILogger<QueueService> _logger;

    public QueueService(
        IOptions<RabbitMqOption> rabbitMqOptions,
        IConnectionFactory factory,
        ILogger<QueueService> logger) :
        base(rabbitMqOptions, factory)
    {
        _logger = logger;
    }

    public Task PublishMessage<T>(T message)
    {
        var serializedMessage = JsonSerializer.Serialize(message);
        var messageBodyBytes = Encoding.UTF8.GetBytes(serializedMessage);

        _logger.LogInformation("Publishing message to {queueName}", QueueName);
        return Task.Run(() => Channel.BasicPublish(ExchangeName, RoutingKey, null, messageBodyBytes));
    }
}