using AutoMapper;
using FluentValidation;
using HotelBackend.Application.Contracts.Infrastructure;
using HotelBackend.Application.Contracts.Persistence;
using HotelBackend.Application.Dtos.Reservations;
using HotelBackend.Application.Exceptions;
using HotelBackend.Application.Features.Reservations.Requests.Commands;
using HotelBackend.Application.Models;
using HotelBackend.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Application.Features.Reservations.Handlers.Commands;

public class UpdateReservationStatusRequestHandler : IRequestHandler<UpdateReservationStatusRequest, Unit>
{
    private readonly ILogger<UpdateReservationStatusRequestHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateReservationPaymentStatusDto> _validator;
    private readonly IEmailQueueService _queueService;
    private readonly IMapper _mapper;

    public UpdateReservationStatusRequestHandler(
        ILogger<UpdateReservationStatusRequestHandler> logger,
        IUnitOfWork unitOfWork,
        IValidator<UpdateReservationPaymentStatusDto> validator,
        IEmailQueueService queueService,
        IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _queueService = queueService;
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

        var reservationDetailsDto = _mapper.Map<GetReservationDetailsDto>(reservation);

        await _queueService.PublishMessage(new ReservationDetailsEmail
        {
            ReservationDetailsDto = reservationDetailsDto,
            ReceiverEmail = reservationDetailsDto.GuestProfile!.ContactEmail,
            Subject = "Payment for reservation"
        });
        _logger.LogInformation("Successfully pushed message to email queue");

        return Unit.Value;
    }
}