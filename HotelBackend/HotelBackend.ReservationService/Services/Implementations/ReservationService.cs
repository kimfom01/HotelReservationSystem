using HotelBackend.ReservationService.Models;
using HotelBackend.ReservationService.Repositories;

namespace HotelBackend.ReservationService.Services.Implementations;

public class ReservationService : IReservationService
{
    private readonly IRoomService _roomService;
    private readonly IUnitOfWork _unitOfWork;

    public ReservationService(
        IRoomService roomService,
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _roomService = roomService;
    }

    public async Task<int> DeleteReservation(int id)
    {
        await _unitOfWork.Reservations.Delete(id);
        return await _unitOfWork.SaveChanges();
    }

    public Task<Reservation?> GetReservation(int id)
    {
        return _unitOfWork.Reservations.GetEntity(id);
    }

    public async Task<IEnumerable<Reservation>?> GetReservations()
    {
        return await _unitOfWork.Reservations.GetEntities(res => true);
    }

    public async Task<Reservation?> MakeReservation(Reservation reservation)
    {
        // var availableRooms = await _roomService
        //     .GetAvailableRoomsPerRoomCapacity(reservation.HotelId, reservation.NumberOfGuests);
        //
        // if (availableRooms is null || !availableRooms.Any())
        // {
        //     return null;
        // }
        //
        // var result = await _unitOfWork.Reservations.Add(reservation);
        //
        //
        // var room = availableRooms.First();
        // room.Availabilty = false;
        // await _unitOfWork.Rooms.Update(room);
        //
        // var reservationRoom = new ReservationRoom
        // {
        //     ReservationId = reservation.Id,
        //     RoomId = room.Id
        // };
        //
        // await _unitOfWork.ReservationRooms.Add(reservationRoom);
        //
        // await _unitOfWork.SaveChanges();
        //
        // return result;

        throw new NotImplementedException();
    }

    public async Task UpdateReservation(Reservation reservation)
    {
        await _unitOfWork.Reservations.Update(reservation);
        await _unitOfWork.SaveChanges();
    }
}