using AutoMapper;
using FluentValidation;
using Hrs.Application.Contracts.Database;
using Hrs.Application.Contracts.MessageBroker;
using Hrs.Application.Contracts.Services;
using Hrs.Application.Dtos.Admin.Rooms;
using Hrs.Application.Dtos.Reservations;
using Hrs.Application.Exceptions;
using Hrs.Common.Messages;
using Hrs.Domain.Entities.Reservation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrs.Application.Features.Reservations.Commands;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, GetReservationDetailsResponse>
{
    private readonly ILogger<CreateReservationCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IReservationsUnitOfWork _reservationsUnitOfWork;
    private readonly IValidator<CreateReservationRequest> _validator;
    private readonly IRoomService _roomService;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateReservationCommandHandler(
        ILogger<CreateReservationCommandHandler> logger,
        IMapper mapper,
        IReservationsUnitOfWork reservationsUnitOfWork,
        IValidator<CreateReservationRequest> validator,
        IRoomService roomService,
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _mapper = mapper;
        _reservationsUnitOfWork = reservationsUnitOfWork;
        _validator = validator;
        _roomService = roomService;
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

        var reserveRoomResponse = await _roomService.PlaceOnHold(new ReserveRoomRequest
        {
            RoomTypeId = command.CreateReservationDto.RoomTypeId,
            HotelId = command.CreateReservationDto.HotelId
        }, cancellationToken);

        if (!reserveRoomResponse.Success)
        {
            _logger.LogError("Unable to place room on hold");
            throw new ReservationException("Unable to place room on hold");
        }

        var guestProfile = GuestProfile.CreateGuestProfile(
            command.CreateReservationDto.GuestProfile.FirstName,
            command.CreateReservationDto.GuestProfile.LastName,
            command.CreateReservationDto.GuestProfile.ContactEmail,
            command.CreateReservationDto.GuestProfile.Sex,
            command.CreateReservationDto.GuestProfile.Age);

        await _reservationsUnitOfWork.GuestProfiles.Add(guestProfile, cancellationToken);

        var reservation = Reservation.CreateReservation(
            command.CreateReservationDto.CheckIn,
            command.CreateReservationDto.CheckOut,
            command.CreateReservationDto.SpecialRequests,
            command.CreateReservationDto.RoomPreferences,
            command.CreateReservationDto.NumberOfGuests,
            guestProfile.Id,
            reserveRoomResponse.RoomId!.Value,
            command.CreateReservationDto.HotelId);

        var added = await _reservationsUnitOfWork.Reservations.Add(reservation, cancellationToken);
        
        await _reservationsUnitOfWork.SaveChanges(cancellationToken);
        _logger.LogInformation("Successfully created a reservation");

        if (added is null)
        {
            _logger.LogError("An error occured: unable to save reservation");

            _logger.LogInformation("Freeing up room");

            await _roomService.FreeUpRoom(
                new FreeRoomRequest
                {
                    HotelId = command.CreateReservationDto.HotelId,
                    RoomId = reservation.RoomId
                }, cancellationToken);

            throw new ReservationException("An error occured: unable to save reservation");
        }

        var reservationMessage = _mapper.Map<ReservationDetails>(added);
        var reservationDetailsDto = _mapper.Map<GetReservationDetailsResponse>(added);

        await _publishEndpoint.Publish(new ReservationCreatedEvent
        {
            ReceiverEmail = reservationMessage.GuestContactEmail,
            Subject = "Reservation created successfully",
            ReceiverName = added.GuestProfile?.FirstName
        }, cancellationToken);

        return reservationDetailsDto;
    }
}