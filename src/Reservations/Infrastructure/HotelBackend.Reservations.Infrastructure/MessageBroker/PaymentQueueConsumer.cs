using AutoMapper;
using FluentValidation;
using HotelBackend.Common.Messages;
using HotelBackend.Reservations.Application.Dtos.Reservations;
using HotelBackend.Reservations.Application.Features.Reservations.Requests.Commands;
using HotelBackend.Reservations.Application.Contracts.Infrastructure.MessageBroker;
using HotelBackend.Reservations.Application.Exceptions;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Reservations.Infrastructure.MessageBroker;

public class PaymentQueueConsumer : IPaymentQueueConsumer
{
    private readonly ILogger<PaymentQueueConsumer> _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public PaymentQueueConsumer(ILogger<PaymentQueueConsumer> logger, IMapper mapper, IMediator mediator)
    {
        _logger = logger;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<PaymentStatusMessage> context)
    {
        var updateReservationPaymentStatusDto =
            _mapper.Map<UpdateReservationPaymentStatusDto>(context.Message);

        try
        {
            await _mediator.Send(new UpdateReservationStatusRequest
            {
                UpdateReservationPaymentStatusDto = updateReservationPaymentStatusDto
            }, context.CancellationToken);
        }
        catch (ValidationException exception)
        {
            _logger.LogError(exception, "ValidationException: {Exception}", exception);
        }
        catch (ReservationException exception)
        {
            _logger.LogError(exception, "ReservationException: {Exception}", exception);
        }
    }
}