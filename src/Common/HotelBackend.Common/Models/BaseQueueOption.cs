namespace HotelBackend.Common.Models;

public abstract class BaseQueueOption
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