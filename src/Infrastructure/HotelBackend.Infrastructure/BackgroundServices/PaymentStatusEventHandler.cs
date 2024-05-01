using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using HotelBackend.Application.Dtos.Reservations;
using HotelBackend.Application.Features.Reservations.Requests.Commands;
using HotelBackend.Application.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HotelBackend.Infrastructure.BackgroundServices;

public class PaymentStatusEventHandler : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<PaymentStatusEventHandler> _logger;
    private readonly RabbitMqOption _rabbitMqOption;

    public PaymentStatusEventHandler(
        IServiceProvider serviceProvider,
        ILogger<PaymentStatusEventHandler> logger,
        IOptions<Config> configOptions
    )
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _rabbitMqOption = configOptions.Value.RabbitMqOption!;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            var scope = _serviceProvider.CreateScope();
            var factory = scope.ServiceProvider.GetRequiredService<IConnectionFactory>();

            factory.Uri =
                new Uri(
                    $"amqp://{_rabbitMqOption.User}:{_rabbitMqOption.Password}@{_rabbitMqOption.Host}:{_rabbitMqOption.Port}");

            factory.ClientProvidedName = _rabbitMqOption.ClientName;

            var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            var exchangeName = _rabbitMqOption.Exchange;
            var routingKey = _rabbitMqOption.RoutingKey;
            var queueName = _rabbitMqOption.QueueName;

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);
            channel.BasicQos(0, 1, false);
            
            if (!stoppingToken.IsCancellationRequested)
            {
                scope = _serviceProvider.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += async (_, args) =>
                {
                    var body = args.Body.ToArray();

                    var json = Encoding.UTF8.GetString(body);

                    Console.WriteLine(json);

                    var updateReservationPaymentStatusDto =
                        JsonSerializer.Deserialize<UpdateReservationPaymentStatusDto>(json);

                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    if (updateReservationPaymentStatusDto is null)
                    {
                        _logger.LogError("Unable to deserialize the update event");
                        throw new SerializationException(
                            "Unable to deserialize the update event");
                    }

                    await mediator.Send(new UpdateReservationStatusRequest
                    {
                        UpdateReservationPaymentStatusDto = updateReservationPaymentStatusDto
                    }, stoppingToken);

                    channel.BasicAck(args.DeliveryTag, false);
                };

                
                _logger.LogInformation("Listening to payment events");
                channel.BasicConsume(queueName, false, consumer);
            }
        }
        catch (Exception exception)
        {
            _logger.LogError("Exception: {exception}", exception.Message);
        }

        return Task.CompletedTask;
    }
}