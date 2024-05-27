using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using AutoMapper;
using FluentValidation;
using HotelBackend.Reservations.Application.Dtos.Reservations;
using HotelBackend.Reservations.Application.Features.Reservations.Requests.Commands;
using HotelBackend.Common.Models;
using HotelBackend.Common.Models.Options;
using HotelBackend.Reservations.Application.Contracts.Infrastructure.MessageBroker;
using HotelBackend.Reservations.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HotelBackend.Reservations.Infrastructure.MessageBroker;

public class PaymentQueueSubscriber : IPaymentQueueSubscriber
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<PaymentQueueSubscriber> _logger;
    private readonly IMapper _mapper;
    private readonly PaymentQueueOptions _queueOptions;

    public PaymentQueueSubscriber(
        IServiceProvider serviceProvider,
        ILogger<PaymentQueueSubscriber> logger,
        IOptions<PaymentQueueOptions> configOptions,
        IMapper mapper
    )
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _mapper = mapper;
        _queueOptions = configOptions.Value;
    }

    public Task SubscribeToQueue(CancellationToken stoppingToken)
    {
        var scope = _serviceProvider.CreateScope();
        var factory = scope.ServiceProvider.GetRequiredService<IConnectionFactory>();

        factory.Uri =
            new Uri(
                $"amqp://{_queueOptions.User}:{_queueOptions.Password}@{_queueOptions.Host}:{_queueOptions.Port}");

        factory.ClientProvidedName = _queueOptions.ClientName;

        var connection = factory.CreateConnection();

        var channel = connection.CreateModel();

        var exchangeName = _queueOptions.Exchange;
        var routingKey = _queueOptions.RoutingKey;
        var queueName = _queueOptions.QueueName;

        channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
        channel.QueueDeclare(queueName, false, false, false, null);
        channel.QueueBind(queueName, exchangeName, routingKey, null);
        channel.BasicQos(0, 1, false);

        try
        {
            scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (_, args) =>
            {
                var body = args.Body.ToArray();

                var json = Encoding.UTF8.GetString(body);

                var paymentStatusMessage = JsonSerializer.Deserialize<PaymentStatusMessage>(json);

                if (paymentStatusMessage is null)
                {
                    _logger.LogError("Unable to deserialize the update event");
                    throw new SerializationException(
                        "Unable to deserialize the update event");
                }

                var updateReservationPaymentStatusDto =
                    _mapper.Map<UpdateReservationPaymentStatusDto>(paymentStatusMessage);

                try
                {
                    await mediator.Send(new UpdateReservationStatusRequest
                    {
                        UpdateReservationPaymentStatusDto = updateReservationPaymentStatusDto
                    }, stoppingToken);
                }
                catch (ValidationException exception)
                {
                    _logger.LogError(exception, "ValidationException: {Exception}", exception);
                }
                catch (ReservationException exception)
                {
                    _logger.LogError(exception, "ReservationException: {Exception}", exception);
                }

                channel.BasicAck(args.DeliveryTag, false);
            };

            _logger.LogInformation("Listening to payment events");
            channel.BasicConsume(queueName, false, consumer);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception: {Exception}", exception);
        }

        return Task.CompletedTask;
    }
}