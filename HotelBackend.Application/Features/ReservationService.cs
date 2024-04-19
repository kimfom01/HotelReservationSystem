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
    private readonly IRoomService _roomService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IQueueService _queueService;

    public ReservationService(
        IRoomService roomService,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IQueueService queueService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _queueService = queueService;
        _roomService = roomService;
    }

    public Task<Reservation?> GetReservation(Guid id)
    {
        return _unitOfWork.Reservations.GetEntity(id);
    }

    public async Task<IEnumerable<Reservation>?> GetReservations()
    {
        return await _unitOfWork.Reservations.GetEntities(res => true);
    }

    public async Task SetPaymentStatus(Guid reservationId, PaymentStatusEnum statusEnum)
    {
        var reservation = await _unitOfWork.Reservations.GetEntity(reservationId);

        if (reservation is null)
        {
            throw new NotFoundException($"Reservation with id={reservationId} does not exist");
        }

        reservation.PaymentStatusEnum = statusEnum;
        await _unitOfWork.SaveChanges();
    }

    public async Task<Reservation> MakeReservation(Reservation reservationDto)
    {
        var reservation = _mapper.Map<Reservation>(reservationDto);
        var room = await _roomService.GetRoom(reservation.RoomId);

        if (room is null)
        {
            throw new NotFoundException($"Room with id={reservation.RoomId} could not be found");
        }

        if (room.Availability != true)
        {
            throw new NotAvailableException($"Room with id={reservation.RoomId} already taken");
        }

        room.Availability = false;

        var currentGuestProfile = await _unitOfWork.GuestProfiles.GetByEmail(reservation.GuestProfile.ContactEmail);

        if (currentGuestProfile is null)
        {
            currentGuestProfile = await _unitOfWork.GuestProfiles.Add(reservation.GuestProfile);
        }
        else
        {
            _mapper.Map(reservation.GuestProfile, currentGuestProfile);

            await _unitOfWork.GuestProfiles.Update(currentGuestProfile);
        }

        if (currentGuestProfile is null)
        {
            throw new ReservationException("An error occured while making reservation");
        }

        reservation.GuestProfileId = currentGuestProfile.Id;

        var added = await _unitOfWork.Reservations.Add(reservation);
        await _unitOfWork.SaveChanges();

        if (added is null)
        {
            throw new ReservationException("An error occured while making reservation");
        }

        var message = _mapper.Map<ReservationMessage>(added);

        // reservationDto = _mapper.Map<ReservationDto>(added);

        await _queueService.PublishMessage(message);

        return added;
    }
}