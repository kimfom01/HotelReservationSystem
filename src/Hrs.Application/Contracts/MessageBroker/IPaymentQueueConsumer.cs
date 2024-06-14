using Hrs.Common.Messages;
using MassTransit;

namespace Hrs.Application.Contracts.MessageBroker;

public interface IPaymentQueueConsumer: IConsumer<PaymentSavedMessage>
{
}