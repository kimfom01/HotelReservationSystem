using AutoMapper;
using FluentValidation;
using HotelBackend.Common.Messages;
using HotelBackend.Reservations.Application.Contracts.ApiServices;
using HotelBackend.Reservations.Application.Contracts.Database;
using HotelBackend.Reservations.Application.Dtos.AdminApi.RoomApi;
using HotelBackend.Reservations.Application.Dtos.Reservations;
using HotelBackend.Reservations.Application.Exceptions;
using HotelBackend.Reservations.Application.Features.Reservations.Requests.Commands;
using HotelBackend.Reservations.Domain.Entities.Reservation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Reservations.Application.Features.Reservations.Handlers.Commands;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, GetReservationDetailsResponse>
{
    private readonly ILogger<CreateReservationCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IReservationsUnitOfWork _reservationsUnitOfWork;
    private readonly IValidator<CreateReservationRequest> _validator;
    private readonly IRoomApiService _roomApiService;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateReservationCommandHandler(
        ILogger<CreateReservationCommandHandler> logger,
        IMapper mapper,
        IReservationsUnitOfWork reservationsUnitOfWork,
        IValidator<CreateReservationRequest> validator,
        IRoomApiService roomApiService,
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _mapper = mapper;
        _reservationsUnitOfWork = reservationsUnitOfWork;
        _validator = validator;
        _roomApiService = roomApiService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<GetReservationDetailsResponse> Handle(CreateReservationCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new reservation");

        if (command.CreateReservationDto is null)
        {
            _logger.LogError("An error occured: {ReservationDto} is null", nameof(CreateReservationRequest));
            throw new ArgumentNullException(nameof(command), $"{nameof(CreateReservationRequest)} is null");
        }

        await _validator.ValidateAndThrowAsync(command.CreateReservationDto, cancellationToken);

        var reservation = _mapper.Map<Reservation>(command.CreateReservationDto);

        var reserveRoomResponse = await _roomApiService.PlaceOnHold(new ReserveRoomApiRequest
        {
            RoomTypeId = command.CreateReservationDto.RoomTypeId,
            HotelId = reservation.HotelId
        }, cancellationToken);

        if (!reserveRoomResponse.Success)
        {
            _logger.LogError("An error occured while marking room as unavailable for reservation={ReservationId}",
                reservation.Id);
            throw new ReservationException("An error occured: check if room is available");
        }

        var newGuest = await _reservationsUnitOfWork.GuestProfiles.Add(reservation.GuestProfile!, cancellationToken);

        reservation.GuestProfileId = newGuest!.Id;

        reservation.RoomId = reserveRoomResponse.RoomId!.Value;
        var added = await _reservationsUnitOfWork.Reservations.Add(reservation, cancellationToken);
        await _reservationsUnitOfWork.SaveChanges(cancellationToken);
        _logger.LogInformation("Successfully created a reservation");

        if (added is null)
        {
            _logger.LogError("An error occured: unable to save reservation");

            _logger.LogInformation("Freeing up room");

            await _roomApiService.FreeUpRoom(
                new FreeRoomApiRequest
                {
                    HotelId = command.CreateReservationDto.HotelId,
                    RoomId = reservation.RoomId
                }, cancellationToken);

            throw new ReservationException("An error occured: unable to save reservation");
        }

        var reservationMessage = _mapper.Map<ReservationDetails>(added);
        var reservationDetailsDto = _mapper.Map<GetReservationDetailsResponse>(added);

        await _publishEndpoint.Publish(new ReservationCreatedMessage
        {
            ReservationMessage = reservationMessage,
            ReceiverEmail = reservationMessage.GuestContactEmail,
            Subject = "Reservation created successfully"
        }, cancellationToken);
        _logger.LogInformation("Successfully pushed message to email queue");

        return reservationDetailsDto;
    }
}