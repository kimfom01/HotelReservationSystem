using AutoMapper;
using FluentValidation;
using HotelBackend.Common.Enums;
using HotelBackend.Common.Models;
using HotelBackend.Payments.Application.Contracts.Infrastructure.Database;
using HotelBackend.Payments.Application.Contracts.Infrastructure.MessageBroker;
using HotelBackend.Payments.Application.Dtos.Payments;
using HotelBackend.Payments.Application.Features.Payments.Requests.Commands;
using HotelBackend.Payments.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Payments.Application.Features.Payments.Handlers.Commands;

public class AddPaymentRequestHandler : IRequestHandler<AddPaymentRequest, GetPaymentDto>
{
    private readonly IMapper _mapper;
    private readonly ILogger<AddPaymentRequestHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddPaymentDto> _validator;
    private readonly IPaymentStatusPublisher _paymentStatusPublisher;

    public AddPaymentRequestHandler(
        IMapper mapper,
        ILogger<AddPaymentRequestHandler> logger,
        IUnitOfWork unitOfWork,
        IValidator<AddPaymentDto> validator,
        IPaymentStatusPublisher paymentStatusPublisher)
    {
        _mapper = mapper;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _paymentStatusPublisher = paymentStatusPublisher;
    }

    public async Task<GetPaymentDto> Handle(AddPaymentRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding payment info");

        if (request.PaymentDto is null)
        {
            throw new ArgumentNullException(nameof(request), "PaymentDto is required");
        }

        var validationResults = await _validator.ValidateAsync(request.PaymentDto, cancellationToken);

        if (!validationResults.IsValid)
        {
            _logger.LogError("Validation failed: {ValidationError}", validationResults.Errors);
            throw new ValidationException(validationResults.Errors);
        }

        var payment = _mapper.Map<Payment>(request.PaymentDto);

        var statuses = Enum.GetValues(typeof(PaymentStatus));

        var rand = new Random();

        var status = (PaymentStatus)(statuses.GetValue(rand.Next(statuses.Length)) ?? PaymentStatus.PENDING);

        payment.Status = status;

        var added = await _unitOfWork.Payments.AddItem(payment);
        await _unitOfWork.SaveChanges();

        var paymentMessage = _mapper.Map<PaymentStatusMessage>(added);

        await _paymentStatusPublisher.PublishMessage(paymentMessage);

        return _mapper.Map<GetPaymentDto>(added);
    }
}