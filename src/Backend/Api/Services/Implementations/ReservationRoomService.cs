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

    public Task<int> DeleteReservationRoom(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ReservationRoom?> GetReservationRoom(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ReservationRoom?> GetReservationRoom(Expression<Func<ReservationRoom, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ReservationRoom>?> GetReservationRooms()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ReservationRoom>?> GetReservationRooms(Expression<Func<ReservationRoom, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task UpdateReservationRoom(ReservationRoom reservationRoom)
    {
        throw new NotImplementedException();
    }
}