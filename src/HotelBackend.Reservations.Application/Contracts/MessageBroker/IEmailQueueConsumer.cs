using HotelBackend.Common.Messages;
using MassTransit;

namespace HotelBackend.Reservations.Application.Contracts.MessageBroker;

public interface IEmailQueueConsumer : IConsumer<ReservationCreatedMessage>
{
}