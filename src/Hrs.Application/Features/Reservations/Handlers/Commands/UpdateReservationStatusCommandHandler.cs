using AutoMapper;
using FluentValidation;
using Hrs.Common.Enums;
using Hrs.Common.Messages;
using Hrs.Application.Contracts.Database;
using Hrs.Application.Contracts.Services;
using Hrs.Application.Dtos.Admin.Rooms;
using Hrs.Application.Dtos.Reservations;
using Hrs.Application.Exceptions;
using Hrs.Application.Features.Reservations.Requests.Commands;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrs.Application.Features.Reservations.Handlers.Commands;

public class UpdateReservationStatusCommandHandler : IRequestHandler<UpdateReservationStatusCommand, Unit>
{
    private readonly ILogger<UpdateReservationStatusCommandHandler> _logger;
    private readonly IReservationsUnitOfWork _reservationsUnitOfWork;
    private readonly IValidator<UpdateReservationPaymentStatusRequest> _validator;
    private readonly IMapper _mapper;
    private readonly IRoomService _roomService;
    private readonly IPublishEndpoint _publishEndpoint;

    public UpdateReservationStatusCommandHandler(
        ILogger<UpdateReservationStatusCommandHandler> logger,
        IReservationsUnitOfWork reservationsUnitOfWork,
        IValidator<UpdateReservationPaymentStatusRequest> validator,
        IMapper mapper,
        IRoomService roomService,
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _reservationsUnitOfWork = reservationsUnitOfWork;
        _validator = validator;
        _mapper = mapper;
        _roomService = roomService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Unit> Handle(UpdateReservationStatusCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating reservation status");

        if (command.UpdateReservationPaymentStatusDto is null)
        {
            _logger.LogError("An error occured: {ReservationDto} is null",
                nameof(UpdateReservationPaymentStatusRequest));
            throw new ArgumentNullException(nameof(command),
                $"{nameof(UpdateReservationPaymentStatusRequest)} is null");
        }

        await _validator.ValidateAndThrowAsync(command.UpdateReservationPaymentStatusDto, cancellationToken);

        var reservation =
            await _reservationsUnitOfWork.Reservations.GetReservationDetails(
                command.UpdateReservationPaymentStatusDto.ReservationId,
                cancellationToken);

        if (reservation is null)
        {
            _logger.LogError("Reservation with id={ReservationId} could not be found",
                command.UpdateReservationPaymentStatusDto.ReservationId);
            throw new NotFoundException(
                $"Reservation with id={command.UpdateReservationPaymentStatusDto.ReservationId} could not be found");
        }

        if (reservation.ReservationStatus == ReservationStatus.Cancelled)
        {
            _logger.LogError("Reservation is already cancelled");
            throw new ReservationException("Reservation is already cancelled");
        }

        reservation.PaymentStatus = command.UpdateReservationPaymentStatusDto.Status;
        reservation.PaymentId = command.UpdateReservationPaymentStatusDto.PaymentId;

        if (command.UpdateReservationPaymentStatusDto.Status == PaymentStatus.Paid)
        {
            reservation.ReservationStatus = ReservationStatus.Confirmed;
        }

        if (command.UpdateReservationPaymentStatusDto.Status is PaymentStatus.Canceled or PaymentStatus.Refunded)
        {
            var roomIsFreed = await _roomService.FreeUpRoom(new FreeRoomRequest
            {
                RoomId = reservation.RoomId,
                HotelId = reservation.HotelId,
            }, cancellationToken);

            if (!roomIsFreed)
            {
                _logger.LogError("An error occured: unable to free up room");
                throw new ReservationException("An error occured: unable to free up room");
            }

            reservation.ReservationStatus = ReservationStatus.Cancelled;
        }

        await _reservationsUnitOfWork.SaveChanges(cancellationToken);
        _logger.LogInformation("Successfully updated reservation");

        var reservationDetailsDto = _mapper.Map<ReservationDetails>(reservation);

        await _publishEndpoint.Publish(new ReservationCreatedMessage
        {
            ReservationMessage = reservationDetailsDto,
            ReceiverEmail = reservationDetailsDto.GuestContactEmail,
            Subject = "Payment for reservation"
        }, cancellationToken);
        _logger.LogInformation("Successfully pushed message to email queue");

        return Unit.Value;
    }
}