using AutoMapper;
using FluentValidation;
using Hrs.Common.Messages;
using Hrs.Domain.Entities.Payment;
using Hrs.Application.Contracts.Database;
using Hrs.Application.Dtos.Payments;
using Hrs.Application.Features.Payments.Requests.Commands;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrs.Application.Features.Payments.Handlers.Commands;

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

        var payment = _mapper.Map<Payment>(command.PaymentRequest);

        payment.Status = command.PaymentRequest.Status;

        var added = await _paymentsUnitOfWork.Payments.Add(payment, cancellationToken);
        await _paymentsUnitOfWork.SaveChanges();

        var paymentMessage = _mapper.Map<PaymentSavedMessage>(added);

        await _publishEndpoint.Publish(paymentMessage, cancellationToken);

        return _mapper.Map<GetPaymentResponse>(added);
    }
}