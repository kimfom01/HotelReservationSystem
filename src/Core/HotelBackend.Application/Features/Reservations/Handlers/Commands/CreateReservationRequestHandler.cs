using AutoMapper;
using FluentValidation;
using HotelBackend.Application.Contracts.Infrastructure;
using HotelBackend.Application.Contracts.Persistence;
using HotelBackend.Application.Dtos.Reservations;
using HotelBackend.Application.Dtos.Reservations.Validators;
using HotelBackend.Application.Exceptions;
using HotelBackend.Application.Features.Reservations.Requests.Commands;
using HotelBackend.Application.Models;
using HotelBackend.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Application.Features.Reservations.Handlers.Commands;

public class CreateReservationRequestHandler : IRequestHandler<CreateReservationRequest, GetReservationDetailsDto>
{
    private readonly ILogger<CreateReservationRequestHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailQueueService _emailQueueService;
    private readonly IValidator<CreateReservationDto> _validator;

    public CreateReservationRequestHandler(
        ILogger<CreateReservationRequestHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IEmailQueueService emailQueueService,
        IValidator<CreateReservationDto> validator)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _emailQueueService = emailQueueService;
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

        var message = _mapper.Map<ReservationMessage>(added);

        await _emailQueueService.PublishMessage(message);
        _logger.LogInformation("Successfully pushed message to email queue");

        return _mapper.Map<GetReservationDetailsDto>(reservation);
    }
}