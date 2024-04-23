namespace HotelBackend.Application.Models;

public class RabbitMqOption
{
    public string User { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Host { get; set; } = string.Empty;

    public int Port { get; set; }

    public string Exchange { get; set; } = string.Empty;

    public string RoutingKey { get; set; } = string.Empty;

    public string QueueName { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
}