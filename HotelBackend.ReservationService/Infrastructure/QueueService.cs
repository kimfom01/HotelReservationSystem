using System.Text;
using System.Text.Json;
using HotelBackend.ReservationService.Dtos;
using HotelBackend.ReservationService.Utils;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace HotelBackend.ReservationService.Infrastructure;

public class QueueService : RabbitMqClientBase
{
    public QueueService(
        IOptions<RabbitMqOption> rabbitMqOptions,
        IConnectionFactory factory) :
        base(rabbitMqOptions, factory)
    {
    }

    public async Task PublishCreateMessage(ReservationDto reservation)
    {
        var message = JsonSerializer.Serialize(reservation);
        byte[] messageBodyBytes = Encoding.UTF8.GetBytes(message);
        await Task.Run(() => Channel.BasicPublish(ExchangeName, RoutingKey, null, messageBodyBytes));
    }
}