using System.Linq.Expressions;
using DataAccess.Models;
using DataAccess.Repositories;

namespace Api.Services.Implementations;

public class ReservationRoomService : IReservationRoomService
{
    private readonly IUnitOfWork _unitOfWork;

    public ReservationRoomService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ReservationRoom?> AddReservationRoom(ReservationRoom reservationRoom)
    {
        var added = await _unitOfWork.ReservationRooms.Add(reservationRoom);

        return added;
    }

    public async Task<int> DeleteReservationRoom(int id)
    {
        await _unitOfWork.ReservationRooms.Delete(id);
        return await _unitOfWork.SaveChanges();
    }

    public async Task<ReservationRoom?> GetReservationRoom(int id)
    {
        return await _unitOfWork.ReservationRooms.GetEntity(id);
    }

    public async Task<ReservationRoom?> GetReservationRoom(Expression<Func<ReservationRoom, bool>> expression)
    {
        return await _unitOfWork.ReservationRooms.GetEntity(expression);
    }

    public async Task<IEnumerable<ReservationRoom>?> GetReservationRooms()
    {
        return await _unitOfWork.ReservationRooms.GetEntities(resroom => true);
    }

    public async Task<IEnumerable<ReservationRoom>?> GetReservationRooms(Expression<Func<ReservationRoom, bool>> expression)
    {
        return await _unitOfWork.ReservationRooms.GetEntities(expression);
    }

    public async Task UpdateReservationRoom(ReservationRoom reservationRoom)
    {
        await _unitOfWork.ReservationRooms.Update(reservationRoom);
        await _unitOfWork.SaveChanges();
    }
}