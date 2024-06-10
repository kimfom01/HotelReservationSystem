using AutoMapper;
using FluentValidation;
using HotelBackend.Common.Messages;
using HotelBackend.Payments.Application.Contracts.Infrastructure.Database;
using HotelBackend.Payments.Application.Dtos.Payments;
using HotelBackend.Payments.Application.Features.Payments.Requests.Commands;
using HotelBackend.Payments.Domain.Entities;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using AddPaymentRequest = HotelBackend.Payments.Application.Dtos.Payments.AddPaymentRequest;

namespace HotelBackend.Payments.Application.Features.Payments.Handlers.Commands;

public class AddPaymentCommandHandler : IRequestHandler<AddPaymentCommand, GetPaymentResponse>
{
    private readonly IMapper _mapper;
    private readonly ILogger<AddPaymentCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddPaymentRequest> _validator;
    private readonly IPublishEndpoint _publishEndpoint;

    public AddPaymentCommandHandler(
        IMapper mapper,
        ILogger<AddPaymentCommandHandler> logger,
        IUnitOfWork unitOfWork,
        IValidator<AddPaymentRequest> validator,
        IPublishEndpoint publishEndpoint)
    {
        _mapper = mapper;
        _logger = logger;
        _unitOfWork = unitOfWork;
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

        var added = await _unitOfWork.Payments.AddItem(payment);
        await _unitOfWork.SaveChanges();

        var paymentMessage = _mapper.Map<PaymentSavedMessage>(added);

        await _publishEndpoint.Publish(paymentMessage, cancellationToken);

        return _mapper.Map<GetPaymentResponse>(added);
    }
}