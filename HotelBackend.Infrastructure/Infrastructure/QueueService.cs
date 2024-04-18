using System.Text;
using System.Text.Json;
using HotelBackend.Infrastructure.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace HotelBackend.Infrastructure.Infrastructure;

public class QueueService<T> : RabbitMqClientBase
{
    public QueueService(
        IOptions<RabbitMqOption> rabbitMqOptions,
        IConnectionFactory factory) :
        base(rabbitMqOptions, factory)
    {
    }

    public Task PublishMessage(T message)
    {
        var serializedMessage = JsonSerializer.Serialize(message);
        var messageBodyBytes = Encoding.UTF8.GetBytes(serializedMessage);

        return Task.Run(() => Channel.BasicPublish(ExchangeName, RoutingKey, null, messageBodyBytes));
    }
}