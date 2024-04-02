using AutoMapper;
using HotelBackend.ReservationService.Data;
using HotelBackend.ReservationService.Exceptions;
using HotelBackend.ReservationService.Infrastructure;
using HotelBackend.ReservationService.Room;

namespace HotelBackend.ReservationService.Reservation;

public class ReservationService : IReservationService
{
    private readonly IRoomService _roomService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly QueueService _queueService;

    public ReservationService(
        IRoomService roomService,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        QueueService queueService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _queueService = queueService;
        _roomService = roomService;
    }

    public Task<ReservationModel?> GetReservation(Guid id)
    {
        return _unitOfWork.Reservations.GetEntity(id);
    }

    public async Task<IEnumerable<ReservationModel>?> GetReservations()
    {
        return await _unitOfWork.Reservations.GetEntities(res => true);
    }

    public async Task<ReservationDto> MakeReservation(ReservationDto reservationDto)
    {
        var reservation = _mapper.Map<ReservationModel>(reservationDto);
        var room = await _roomService.GetRoom(reservation.RoomId);

        if (room is null)
        {
            throw new NotFoundException($"Room with id={reservation.RoomId} could not be found");
        }

        if (room.Availability != true)
        {
            throw new Exception($"Room with id={reservation.RoomId} already taken");
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
            throw new Exception("An error occured while making reservation");
        }

        reservation.GuestProfileId = currentGuestProfile.Id;

        var added = await _unitOfWork.Reservations.Add(reservation);
        await _unitOfWork.SaveChanges();

        if (added is null)
        {
            throw new Exception("An error occured while making reservation");
        }

        reservationDto = _mapper.Map<ReservationDto>(added);

        await _queueService.PublishCreateMessage(reservationDto);

        return reservationDto;
    }
}