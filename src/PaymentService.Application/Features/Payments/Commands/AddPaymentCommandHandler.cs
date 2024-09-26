using AutoMapper;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PaymentService.Application.Contracts.Database;
using PaymentService.Application.Contracts.MessageBroker;
using PaymentService.Application.Dtos.Payments;
using PaymentService.Domain.Payment;

namespace PaymentService.Application.Features.Payments.Commands;

public class AddPaymentCommandHandler : IRequestHandler<AddPaymentCommand, GetPaymentResponse>
{
    private readonly IMapper _mapper;
    private readonly ILogger<AddPaymentCommandHandler> _logger;
    private readonly IPaymentsUnitOfWork _paymentsUnitOfWork;
    private readonly IValidator<AddPaymentRequest> _validator;
    private readonly IPublishEndpoint _publishEndpoint;

    public AddPaymentCommandHandler(
        IMapper mapper,
        ILogger<AddPaymentCommandHandler> logger,
        IPaymentsUnitOfWork paymentsUnitOfWork,
        IValidator<AddPaymentRequest> validator,
        IPublishEndpoint publishEndpoint)
    {
        _mapper = mapper;
        _logger = logger;
        _paymentsUnitOfWork = paymentsUnitOfWork;
        _validator = validator;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<GetPaymentResponse> Handle(AddPaymentCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding payment info");

        if (command.PaymentRequest is null)
        {
            _logger.LogError("{PaymentDto} is required", nameof(command.PaymentRequest));
            throw new ArgumentNullException(nameof(command), $"{nameof(command.PaymentRequest)} is required");
        }

        await _validator.ValidateAndThrowAsync(command.PaymentRequest, cancellationToken);

        var payment = Payment.CreatePayment(
            command.PaymentRequest.Amount,
            command.PaymentRequest.ReservationId,
            command.PaymentRequest.Status);

        var added = await _paymentsUnitOfWork.Payments.Add(payment, cancellationToken);
        await _paymentsUnitOfWork.SaveChanges();

        var paymentMessage = _mapper.Map<PaymentSavedEvent>(added);

        await _publishEndpoint.Publish(paymentMessage, cancellationToken);

        return _mapper.Map<GetPaymentResponse>(added);
    }
}