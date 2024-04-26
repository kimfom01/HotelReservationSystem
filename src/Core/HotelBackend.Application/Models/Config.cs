namespace HotelBackend.Application.Models;

public class Config
{
    public EmailOption? EmailOption { get; set; }
    public EmailQueueOption? EmailQueueOption { get; set; }
    public RabbitMqOption? RabbitMqOption { get; set; }
}