using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using HotelBackend.Application.Contracts.Infrastructure;
using HotelBackend.Common.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HotelBackend.Infrastructure.Listeners;

public class EmailerListener : IDisposable
{
    private readonly ILogger<EmailerListener> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private string _consumerTag = string.Empty;
    private readonly string _queueName;

    public EmailerListener(
        IOptions<Config> configOptions,
        IConnectionFactory factory,
        ILogger<EmailerListener> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        var emailQueueOption = configOptions.Value.EmailQueueOption;
        factory.Uri =
            new Uri(
                $"amqp://{emailQueueOption!.User}:{emailQueueOption.Password}@{emailQueueOption.Host}:{emailQueueOption.Port}");

        factory.ClientProvidedName = emailQueueOption.ClientName;

        _connection = factory.CreateConnection();

        _channel = _connection.CreateModel();

        _queueName = emailQueueOption.QueueName;

        _channel.ExchangeDeclare(emailQueueOption.Exchange, ExchangeType.Direct);
        _channel.QueueDeclare(_queueName, false, false, false, null);
        _channel.QueueBind(_queueName, emailQueueOption.Exchange, emailQueueOption.RoutingKey, null);
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

                    Console.WriteLine(json);

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