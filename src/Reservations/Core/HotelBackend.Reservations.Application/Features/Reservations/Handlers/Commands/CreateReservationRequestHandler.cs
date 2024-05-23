using AutoMapper;
using FluentValidation;
using HotelBackend.Common.Models;
using HotelBackend.Reservations.Domain.Entities;
using HotelBackend.Reservations.Application.Contracts.Infrastructure.Database;
using HotelBackend.Reservations.Application.Contracts.Infrastructure.MessageBroker;
using HotelBackend.Reservations.Application.Dtos.Reservations;
using HotelBackend.Reservations.Application.Exceptions;
using HotelBackend.Reservations.Application.Features.Reservations.Requests.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Reservations.Application.Features.Reservations.Handlers.Commands;

public class CreateReservationRequestHandler : IRequestHandler<CreateReservationRequest, GetReservationDetailsDto>
{
    private readonly ILogger<CreateReservationRequestHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailQueuePublisher _emailQueuePublisher;
    private readonly IValidator<CreateReservationDto> _validator;

    public CreateReservationRequestHandler(
        ILogger<CreateReservationRequestHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IEmailQueuePublisher emailQueuePublisher,
        IValidator<CreateReservationDto> validator)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _emailQueuePublisher = emailQueuePublisher;
        _validator = validator;
    }

    public async Task<GetReservationDetailsDto> Handle(CreateReservationRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new reservation");

        if (request.CreateReservationDto is null)
        {
            throw new ArgumentNullException(nameof(request), $"{nameof(CreateReservationDto)} is null");
        }

        var validationResult = await _validator.ValidateAsync(request.CreateReservationDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var reservation = _mapper.Map<Reservation>(request.CreateReservationDto);

        var room = await _unitOfWork.Rooms.GetEntity(r => r.Id == reservation.RoomId, cancellationToken);

        if (room is null)
        {
            throw new NotFoundException($"Room with id={reservation.RoomId} could not be found");
        }

        if (!room.Availability)
        {
            throw new NotAvailableException($"Room with id={reservation.RoomId} already taken");
        }

        room.Availability = false;

        var newGuest = await _unitOfWork.GuestProfiles.Add(reservation.GuestProfile!, cancellationToken);

        reservation.GuestProfileId = newGuest!.Id;

        var added = await _unitOfWork.Reservations.Add(reservation, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        _logger.LogInformation("Successfully created a reservation");

        if (added is null)
        {
            throw new ReservationException("An error occured: unable to save reservation");
        }

        var reservationMessage = _mapper.Map<ReservationMessage>(added);
        var reservationDetailsDto = _mapper.Map<GetReservationDetailsDto>(added);

        await _emailQueuePublisher.PublishMessage(new ReservationDetailsEmail
        {
            ReservationMessage = reservationMessage,
            ReceiverEmail = reservationMessage.GuestContactEmail,
            Subject = "Reservation created successfully"
        });
        _logger.LogInformation("Successfully pushed message to email queue");

        return reservationDetailsDto;
    }
}