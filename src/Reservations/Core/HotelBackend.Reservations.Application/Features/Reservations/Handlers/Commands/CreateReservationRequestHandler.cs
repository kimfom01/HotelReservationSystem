using AutoMapper;
using FluentValidation;
using HotelBackend.Common.Models;
using HotelBackend.Reservations.Application.Contracts.ApiServices;
using HotelBackend.Reservations.Domain.Entities;
using HotelBackend.Reservations.Application.Contracts.Infrastructure.Database;
using HotelBackend.Reservations.Application.Contracts.Infrastructure.MessageBroker;
using HotelBackend.Reservations.Application.Dtos.AdminApi.RoomApi;
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
    private readonly IRoomApiService _roomApiService;

    public CreateReservationRequestHandler(
        ILogger<CreateReservationRequestHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IEmailQueuePublisher emailQueuePublisher,
        IValidator<CreateReservationDto> validator,
        IRoomApiService roomApiService)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _emailQueuePublisher = emailQueuePublisher;
        _validator = validator;
        _roomApiService = roomApiService;
    }

    public async Task<GetReservationDetailsDto> Handle(CreateReservationRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new reservation");

        if (request.CreateReservationDto is null)
        {
            _logger.LogError("An error occured: {ReservationDto} is null", nameof(CreateReservationDto));
            throw new ArgumentNullException(nameof(request), $"{nameof(CreateReservationDto)} is null");
        }

        var validationResult = await _validator.ValidateAsync(request.CreateReservationDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Error occured while validating request: {Errors}", validationResult.Errors);
            throw new ValidationException(validationResult.Errors);
        }

        var reservation = _mapper.Map<Reservation>(request.CreateReservationDto);

        var reserveRoomResponse = await _roomApiService.PlaceOnHold(new ReserveRoomRequestDto
        {
            RoomTypeId = request.CreateReservationDto.RoomTypeId,
            HotelId = reservation.HotelId
        });

        if (!reserveRoomResponse.Success)
        {
            _logger.LogError("An error occured while marking room as unavailable for reservation={ReservationId}",
                reservation.Id);
            throw new ReservationException("An error occured: check if room is available");
        }

        var newGuest = await _unitOfWork.GuestProfiles.Add(reservation.GuestProfile!, cancellationToken);

        reservation.GuestProfileId = newGuest!.Id;

        reservation.RoomId = reserveRoomResponse.RoomId!.Value;
        var added = await _unitOfWork.Reservations.Add(reservation, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        _logger.LogInformation("Successfully created a reservation");

        if (added is null)
        {
            _logger.LogError("An error occured: unable to save reservation");
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