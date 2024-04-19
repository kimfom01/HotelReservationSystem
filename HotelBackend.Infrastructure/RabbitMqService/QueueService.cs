using System.Text;
using System.Text.Json;
using HotelBackend.Application.Contracts.Infrastructure;
using HotelBackend.Application.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace HotelBackend.Infrastructure.RabbitMqService;

public class QueueService : RabbitMqClientBase, IQueueService
{
    public QueueService(
        IOptions<RabbitMqOption> rabbitMqOptions,
        IConnectionFactory factory) :
        base(rabbitMqOptions, factory)
    {
    }

    public Task PublishMessage<T>(T message)
    {
        var serializedMessage = JsonSerializer.Serialize(message);
        var messageBodyBytes = Encoding.UTF8.GetBytes(serializedMessage);

        return Task.Run(() => Channel.BasicPublish(ExchangeName, RoutingKey, null, messageBodyBytes));
    }
}