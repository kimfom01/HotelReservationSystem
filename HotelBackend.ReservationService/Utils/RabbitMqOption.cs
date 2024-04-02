namespace HotelBackend.ReservationService.Utils;

public class RabbitMqOption
{
    public string User { get; set; }

    public string Password { get; set; }

    public string Host { get; set; }

    public int Port { get; set; }

    public string Exchange { get; set; }

    public string RoutingKey { get; set; }

    public string QueueName { get; set; }
    public string ClientName { get; set; }
}