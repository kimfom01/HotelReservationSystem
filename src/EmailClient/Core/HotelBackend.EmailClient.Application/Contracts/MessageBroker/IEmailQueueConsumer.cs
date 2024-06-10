using HotelBackend.Common.Messages;
using MassTransit;

namespace HotelBackend.EmailClient.Application.Contracts.MessageBroker;

public interface IEmailQueueConsumer : IConsumer<ReservationCreatedMessage>
{
}