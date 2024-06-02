using AutoMapper;
using FluentValidation;
using HotelBackend.Common.Enums;
using HotelBackend.Common.Models;
using HotelBackend.Reservations.Application.Contracts.ApiServices;
using HotelBackend.Reservations.Application.Contracts.Infrastructure.Database;
using HotelBackend.Reservations.Application.Contracts.Infrastructure.MessageBroker;
using HotelBackend.Reservations.Application.Dtos.AdminApi.RoomApi;
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
    private readonly IRoomApiService _roomApiService;

    public UpdateReservationStatusRequestHandler(
        ILogger<UpdateReservationStatusRequestHandler> logger,
        IUnitOfWork unitOfWork,
        IValidator<UpdateReservationPaymentStatusDto> validator,
        IEmailQueuePublisher queuePublisher,
        IMapper mapper,
        IRoomApiService roomApiService)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _queuePublisher = queuePublisher;
        _mapper = mapper;
        _roomApiService = roomApiService;
    }

    public async Task<Unit> Handle(UpdateReservationStatusRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating reservation status");

        if (request.UpdateReservationPaymentStatusDto is null)
        {
            _logger.LogError("An error occured: {ReservationDto} is null", nameof(UpdateReservationPaymentStatusDto));
            throw new ArgumentNullException(nameof(request), $"{nameof(UpdateReservationPaymentStatusDto)} is null");
        }

        var validationResult =
            await _validator.ValidateAsync(request.UpdateReservationPaymentStatusDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Error occured while validating request: {Errors}", validationResult.Errors);
            throw new ValidationException(validationResult.Errors);
        }

        var reservation =
            await _unitOfWork.Reservations.GetReservationDetails(
                request.UpdateReservationPaymentStatusDto.ReservationId,
                cancellationToken);

        if (reservation is null)
        {
            _logger.LogError("Reservation with id={ReservationId} could not be found",
                request.UpdateReservationPaymentStatusDto.ReservationId);
            throw new NotFoundException(
                $"Reservation with id={request.UpdateReservationPaymentStatusDto.ReservationId} could not be found");
        }

        if (reservation.ReservationStatus == ReservationStatus.Cancelled)
        {
            _logger.LogError("Reservation is already cancelled");
            throw new ReservationException("Reservation is already cancelled");
        }

        reservation.PaymentStatus = request.UpdateReservationPaymentStatusDto.Status;
        reservation.PaymentId = request.UpdateReservationPaymentStatusDto.PaymentId;

        if (request.UpdateReservationPaymentStatusDto.Status == PaymentStatus.Paid)
        {
            reservation.ReservationStatus = ReservationStatus.Confirmed;
        }

        if (request.UpdateReservationPaymentStatusDto.Status is PaymentStatus.Canceled or PaymentStatus.Refunded)
        {
            var roomIsFreed = await _roomApiService.FreeUpRoom(new FreeRoomRequestDto
            {
                RoomId = reservation.RoomId,
                HotelId = reservation.HotelId,
            });

            if (!roomIsFreed)
            {
                _logger.LogError("An error occured: unable to free up room");
                throw new ReservationException("An error occured: unable to free up room");
            }

            reservation.ReservationStatus = ReservationStatus.Cancelled;
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