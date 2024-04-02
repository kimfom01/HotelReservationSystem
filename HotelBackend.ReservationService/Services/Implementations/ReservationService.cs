using AutoMapper;
using HotelBackend.ReservationService.Dtos;
using HotelBackend.ReservationService.Models;
using HotelBackend.ReservationService.Repositories;

namespace HotelBackend.ReservationService.Services.Implementations;

public class ReservationService : IReservationService
{
    private readonly IRoomService _roomService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ReservationService(
        IRoomService roomService,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _roomService = roomService;
    }

    public async Task<int> DeleteReservation(Guid id)
    {
        await _unitOfWork.Reservations.Delete(id);
        return await _unitOfWork.SaveChanges();
    }

    public Task<Reservation?> GetReservation(Guid id)
    {
        return _unitOfWork.Reservations.GetEntity(id);
    }

    public async Task<IEnumerable<Reservation>?> GetReservations()
    {
        return await _unitOfWork.Reservations.GetEntities(res => true);
    }

    public async Task<ReservationDto?> MakeReservation(ReservationDto reservationDto)
    {
        var reservation = _mapper.Map<Reservation>(reservationDto);
        var room = await _roomService.GetRoom(reservation.RoomId);

        if (room is not null && room.Availability == true)
        {
            room.Availability = false;
        }

        var currentGuestProfile = await _unitOfWork.GuestProfiles.GetByEmail(reservation.GuestProfile.ContactEmail);

        if (currentGuestProfile is null)
        {
            await _unitOfWork.GuestProfiles.Add(reservation.GuestProfile);
        }
        
        var added = await _unitOfWork.Reservations.Add(reservation);

        return _mapper.Map<ReservationDto>(added);
    }

    public async Task UpdateReservation(Reservation reservation)
    {
        await _unitOfWork.Reservations.Update(reservation);
        await _unitOfWork.SaveChanges();
    }
}