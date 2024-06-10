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

namespace HotelBackend.Payments.Application.Features.Payments.Handlers.Commands;

public class AddPaymentRequestHandler : IRequestHandler<AddPaymentRequest, GetPaymentDto>
{
    private readonly IMapper _mapper;
    private readonly ILogger<AddPaymentRequestHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddPaymentDto> _validator;
    private readonly IPublishEndpoint _publishEndpoint;

    public AddPaymentRequestHandler(
        IMapper mapper,
        ILogger<AddPaymentRequestHandler> logger,
        IUnitOfWork unitOfWork,
        IValidator<AddPaymentDto> validator,
        IPublishEndpoint publishEndpoint)
    {
        _mapper = mapper;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<GetPaymentDto> Handle(AddPaymentRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding payment info");

        if (request.PaymentDto is null)
        {
            _logger.LogError("{PaymentDto} is required", nameof(request.PaymentDto));
            throw new ArgumentNullException(nameof(request), $"{nameof(request.PaymentDto)} is required");
        }

        await _validator.ValidateAndThrowAsync(request.PaymentDto, cancellationToken);

        var payment = _mapper.Map<Payment>(request.PaymentDto);

        payment.Status = request.PaymentDto.Status;

        var added = await _unitOfWork.Payments.AddItem(payment);
        await _unitOfWork.SaveChanges();

        var paymentMessage = _mapper.Map<PaymentSavedMessage>(added);

        await _publishEndpoint.Publish(paymentMessage, cancellationToken);

        return _mapper.Map<GetPaymentDto>(added);
    }
}