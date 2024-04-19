using AutoMapper;
using HotelBackend.Application.Contracts.Features;
using HotelBackend.Application.Contracts.Infrastructure;
using HotelBackend.Application.Contracts.Persistence;
using HotelBackend.Application.Exceptions;
using HotelBackend.Application.Models;
using HotelBackend.Domain.Entities;
using HotelBackend.Domain.Enums;

namespace HotelBackend.Application.Features;

public class ReservationService : IReservationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IQueueService _queueService;

    public ReservationService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IQueueService queueService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _queueService = queueService;
    }

    public Task<Reservation?> GetReservation(Guid id, CancellationToken cancellationToken)
    {
        return _unitOfWork.Reservations.GetEntity(id, cancellationToken);
    }

    public async Task<IEnumerable<Reservation>?> GetReservations(CancellationToken cancellationToken)
    {
        return await _unitOfWork.Reservations.GetEntities(res => true, cancellationToken);
    }

    public async Task SetPaymentStatus(Guid reservationId, PaymentStatusEnum statusEnum,
        CancellationToken cancellationToken)
    {
        var reservation = await _unitOfWork.Reservations.GetEntity(reservationId, cancellationToken);

        if (reservation is null)
        {
            throw new NotFoundException($"Reservation with id={reservationId} does not exist");
        }

        reservation.PaymentStatusEnum = statusEnum;
        await _unitOfWork.SaveChanges(cancellationToken);
    }

    public async Task<Reservation> MakeReservation(Reservation reservationDto, CancellationToken cancellationToken)
    {
        // Use fluent validator to validate the reservation request dto IDs
        // like check if room is available or return validation errors
        //
        // Also validate guest profile that comes with the request

        var reservation = _mapper.Map<Reservation>(reservationDto);
        var room = await _unitOfWork.Rooms.GetEntity(reservation.RoomId, cancellationToken);

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

        if (added is null)
        {
            throw new ReservationException("An error occured while making reservation");
        }

        var message = _mapper.Map<ReservationMessage>(added);

        await _queueService.PublishMessage(message);

        return added;
    }
}