using AutoMapper;
using FluentValidation;
using HotelBackend.Common.Enums;
using HotelBackend.Common.Models;
using HotelBackend.Reservations.Application.Contracts.Infrastructure.Database;
using HotelBackend.Reservations.Application.Contracts.Infrastructure.MessageBroker;
using HotelBackend.Reservations.Application.Dtos.Reservations;
using HotelBackend.Reservations.Application.Exceptions;
using HotelBackend.Reservations.Application.Features.Reservations.Requests.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Reservations.Application.Features.Reservations.Handlers.Commands;

public class UpdateReservationStatusRequestHandler : IRequestHandler<UpdateReservationStatusRequest, Unit>
{
    private readonly ILogger<UpdateReservationStatusRequestHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateReservationPaymentStatusDto> _validator;
    private readonly IEmailQueuePublisher _queuePublisher;
    private readonly IMapper _mapper;

    public UpdateReservationStatusRequestHandler(
        ILogger<UpdateReservationStatusRequestHandler> logger,
        IUnitOfWork unitOfWork,
        IValidator<UpdateReservationPaymentStatusDto> validator,
        IEmailQueuePublisher queuePublisher,
        IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _queuePublisher = queuePublisher;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateReservationStatusRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating reservation status");

        if (request.UpdateReservationPaymentStatusDto is null)
        {
            throw new ArgumentNullException(nameof(request), $"{nameof(UpdateReservationPaymentStatusDto)} is null");
        }

        var validationResult =
            await _validator.ValidateAsync(request.UpdateReservationPaymentStatusDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var reservation =
            await _unitOfWork.Reservations.GetReservationDetails(
                request.UpdateReservationPaymentStatusDto.ReservationId,
                cancellationToken);

        if (reservation is null)
        {
            throw new NotFoundException(
                $"Reservation with id={request.UpdateReservationPaymentStatusDto.ReservationId} could not be found");
        }

        reservation.PaymentStatus = request.UpdateReservationPaymentStatusDto.Status;
        reservation.PaymentId = request.UpdateReservationPaymentStatusDto.PaymentId;

        if (request.UpdateReservationPaymentStatusDto.Status == PaymentStatus.PAID)
        {
            reservation.ReservationStatus = ReservationStatus.CONFIRMED;
        }

        if (request.UpdateReservationPaymentStatusDto.Status == PaymentStatus.CANCELED ||
            request.UpdateReservationPaymentStatusDto.Status == PaymentStatus.REFUNDED)
        {
            reservation.ReservationStatus = ReservationStatus.CANCELLED;
        }

        await _unitOfWork.SaveChanges(cancellationToken);
        _logger.LogInformation("Successfully updated reservation");
        
        var reservationDetailsDto = _mapper.Map<ReservationMessage>(reservation);

        await _queuePublisher.PublishMessage(new ReservationDetailsEmail
        {
            ReservationMessage = reservationDetailsDto,
            ReceiverEmail = reservationDetailsDto.GuestContactEmail,
            Subject = "Payment for reservation"
        });
        _logger.LogInformation("Successfully pushed message to email queue");

        return Unit.Value;
    }
}