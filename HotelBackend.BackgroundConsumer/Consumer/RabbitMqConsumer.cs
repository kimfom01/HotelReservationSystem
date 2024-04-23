using System.Text;
using System.Text.Json;
using HotelBackend.Application.Dtos.Reservations;
using HotelBackend.Application.Exceptions;
using HotelBackend.Application.Features.Reservations.Requests.Commands;
using HotelBackend.Application.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HotelBackend.BackgroundConsumer.Consumer;

public class RabbitMqConsumer : IDisposable
{
    private readonly IMediator _mediator;
    private readonly ILogger<RabbitMqConsumer> _logger;
    private readonly IConnection _connection;
    private readonly RabbitMqOption _rabbitMqOption;
    private readonly IModel _channel;
    private string _consumerTag = string.Empty;

    public RabbitMqConsumer(
        IOptions<RabbitMqOption> options,
        IConnectionFactory factory,
        IMediator mediator,
        ILogger<RabbitMqConsumer> logger)
    {
        _mediator = mediator;
        _logger = logger;
        _rabbitMqOption = options.Value;
        factory.Uri =
            new Uri(
                $"amqp://{_rabbitMqOption.User}:{_rabbitMqOption.Password}@{_rabbitMqOption.Host}:{_rabbitMqOption.Port}");

        factory.ClientProvidedName = _rabbitMqOption.ClientName;

        _connection = factory.CreateConnection();

        _channel = _connection.CreateModel();
    }

    public Task ExecuteAsync(CancellationToken stoppingToken = default)
    {
        var exchangeName = _rabbitMqOption.Exchange;
        var routingKey = _rabbitMqOption.RoutingKey;
        var queueName = _rabbitMqOption.QueueName;

        _channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
        _channel.QueueDeclare(queueName, false, false, false, null);
        _channel.QueueBind(queueName, exchangeName, routingKey, null);
        _channel.BasicQos(0, 1, false);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += async (_, args) =>
                {
                    var body = args.Body.ToArray();

                    var json = Encoding.UTF8.GetString(body);

                    Console.WriteLine(json);

                    var updateReservationPaymentStatusDto =
                        JsonSerializer.Deserialize<UpdateReservationPaymentStatusDto>(json);

                    await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
                    await _mediator.Send(new UpdateReservationStatusRequest
                    {
                        UpdateReservationPaymentStatusDto = updateReservationPaymentStatusDto ??
                                                            throw new ReservationException(
                                                                "Unable to deserialize the update event")
                    }, stoppingToken);

                    _channel.BasicAck(args.DeliveryTag, false);
                };

                _consumerTag = _channel.BasicConsume(queueName, false, consumer);
            }
        }
        catch (Exception exception)
        {
            _logger.LogError("Exception: {exception}", exception.Message);
        }

        return Task.CompletedTask;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _channel.BasicCancel(_consumerTag);

            _channel.Close();
            _connection.Close();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}