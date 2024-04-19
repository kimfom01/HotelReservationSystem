using System.Text;
using HotelBackend.Application.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HotelBackend.BackgroundConsumer.Consumer;

public sealed class RabbitMqConsumer : BackgroundService
{
    private readonly IConnection _connection;
    private readonly RabbitMqOption _rabbitMqOption;
    private readonly IModel _channel;
    private string _consumerTag = string.Empty;

    public RabbitMqConsumer(
        IOptions<RabbitMqOption> options,
        IConnectionFactory factory)
    {
        _rabbitMqOption = options.Value;
        factory.Uri =
            new Uri(
                $"amqp://{_rabbitMqOption.User}:{_rabbitMqOption.Password}@{_rabbitMqOption.Host}:{_rabbitMqOption.Port}");

        factory.ClientProvidedName = _rabbitMqOption.ClientName;

        _connection = factory.CreateConnection();

        _channel = _connection.CreateModel();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var exchangeName = _rabbitMqOption.Exchange;
        var routingKey = _rabbitMqOption.RoutingKey;
        var queueName = _rabbitMqOption.QueueName;

        _channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
        _channel.QueueDeclare(queueName, false, false, false, null);
        _channel.QueueBind(queueName, exchangeName, routingKey, null);
        _channel.BasicQos(0, 1, false);

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (sender, args) =>
        {
            var body = args.Body.ToArray();

            var json = Encoding.UTF8.GetString(body);
            
            Console.WriteLine(json);

            // var paymentStatus = JsonSerializer.Deserialize<PaymentStatus>(json);

            _channel.BasicAck(args.DeliveryTag, false);
        };

        _consumerTag = _channel.BasicConsume(queueName, false, consumer);

        return Task.CompletedTask;
    }

    private void Cleanup(bool disposing)
    {
        if (disposing)
        {
            _channel.BasicCancel(_consumerTag);

            _channel.Close();
            _connection.Close();
        }
    }

    public override void Dispose()
    {
        Cleanup(true);
        base.Dispose();
    }
}