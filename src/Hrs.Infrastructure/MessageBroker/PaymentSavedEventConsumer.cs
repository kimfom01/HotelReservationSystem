using AutoMapper;
using FluentValidation;
using Hrs.Application.Contracts.MessageBroker;
using Hrs.Application.Dtos.Reservations;
using Hrs.Application.Exceptions;
using Hrs.Application.Features.Reservations.Commands;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrs.Infrastructure.MessageBroker;

public class PaymentSavedEventConsumer : IConsumer<PaymentSavedEvent>
{
    private readonly ILogger<PaymentSavedEventConsumer> _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public PaymentSavedEventConsumer(ILogger<PaymentSavedEventConsumer> logger, IMapper mapper, IMediator mediator)
    {
        _logger = logger;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<PaymentSavedEvent> context)
    {
        _logger.LogInformation("Consuming {EventName} event", nameof(PaymentSavedEvent));
        
        var updateReservationPaymentStatusDto =
            _mapper.Map<UpdateReservationPaymentStatusRequest>(context.Message);

        try
        {
            await _mediator.Send(new UpdateReservationStatusCommand
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