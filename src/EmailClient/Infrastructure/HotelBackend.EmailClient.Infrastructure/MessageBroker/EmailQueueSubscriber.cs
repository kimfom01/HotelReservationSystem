using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using HotelBackend.Common.Models;
using HotelBackend.Common.Models.Options;
using HotelBackend.EmailClient.Application.Contracts.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HotelBackend.EmailClient.Infrastructure.MessageBroker;

public class EmailQueueSubscriber : IDisposable
{
    private readonly ILogger<EmailQueueSubscriber> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private string _consumerTag = string.Empty;
    private readonly string _queueName;

    public EmailQueueSubscriber(
        IOptions<EmailQueueOptions> configOptions,
        IConnectionFactory factory,
        ILogger<EmailQueueSubscriber> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        var queueOptions = configOptions.Value;
        factory.Uri =
            new Uri(
                $"amqp://{queueOptions.User}:{queueOptions.Password}@{queueOptions.Host}:{queueOptions.Port}");

        factory.ClientProvidedName = queueOptions.ClientName;

        _connection = factory.CreateConnection();

        _channel = _connection.CreateModel();

        _queueName = queueOptions.QueueName;

        _channel.ExchangeDeclare(queueOptions.Exchange, ExchangeType.Direct);
        _channel.QueueDeclare(_queueName, false, false, false, null);
        _channel.QueueBind(_queueName, queueOptions.Exchange, queueOptions.RoutingKey, null);
        _channel.BasicQos(0, 1, false);
    }

    public Task ExecuteAsync(CancellationToken stoppingToken = default)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var consumer = new EventingBasicConsumer(_channel);

                var scope = _serviceProvider.CreateScope();

                var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                consumer.Received += async (_, args) =>
                {
                    var body = args.Body.ToArray();

                    var json = Encoding.UTF8.GetString(body);

                    var reservationDetailsEmail =
                        JsonSerializer.Deserialize<ReservationDetailsEmail>(json);

                    if (reservationDetailsEmail is null && reservationDetailsEmail!.ReservationMessage is null)
                    {
                        throw new SerializationException("Unable to deserialize the update event");
                    }

                    await emailSender.SendEmailAsync(
                        reservationDetailsEmail.ReceiverEmail,
                        reservationDetailsEmail.Subject,
                        reservationDetailsEmail.ReservationMessage!);

                    _channel.BasicAck(args.DeliveryTag, false);
                };

                _consumerTag = _channel.BasicConsume(_queueName, false, consumer);
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception,"Exception: {Exception}", exception);
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