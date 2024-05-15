using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using HotelBackend.Reservations.Application.Contracts.Infrastructure;
using HotelBackend.Reservations.Application.Dtos.Reservations;
using HotelBackend.Reservations.Application.Features.Reservations.Requests.Commands;
using HotelBackend.Common.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HotelBackend.Reservations.Infrastructure.MessageBroker;

public class EmailQueueSubscriber : IEmailQueueSubscriber
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<EmailQueueSubscriber> _logger;
    private readonly PaymentQueueOption _paymentQueueOption;

    public EmailQueueSubscriber(
        IServiceProvider serviceProvider,
        ILogger<EmailQueueSubscriber> logger,
        IOptions<Config> configOptions
    )
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _paymentQueueOption = configOptions.Value.PaymentQueueOption!;
    }

    public Task SubcribeToQueue(CancellationToken stoppingToken)
    {
        try
        {
            var scope = _serviceProvider.CreateScope();
            var factory = scope.ServiceProvider.GetRequiredService<IConnectionFactory>();

            factory.Uri =
                new Uri(
                    $"amqp://{_paymentQueueOption.User}:{_paymentQueueOption.Password}@{_paymentQueueOption.Host}:{_paymentQueueOption.Port}");

            factory.ClientProvidedName = _paymentQueueOption.ClientName;

            var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            var exchangeName = _paymentQueueOption.Exchange;
            var routingKey = _paymentQueueOption.RoutingKey;
            var queueName = _paymentQueueOption.QueueName;

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);
            channel.BasicQos(0, 1, false);


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
        catch (Exception exception)
        {
            _logger.LogError("Exception: {exception}", exception.Message);
        }

        return Task.CompletedTask;
    }
}