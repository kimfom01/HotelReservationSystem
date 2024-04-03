using System.Linq.Expressions;
using HotelBackend.Old.Remove.DataAccess.Models;
using HotelBackend.Old.Remove.DataAccess.Repositories;

namespace HotelBackend.Old.Remove.Api.Services.Implementations;

public class RoomStatusService : IRoomStatusService
{
    private readonly IUnitOfWork _unitOfWork;

    public RoomStatusService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<RoomStatus?> GetRoomStatus(int id)
    {
        return await _unitOfWork.RoomStatuses.GetEntity(id);
    }

    public async Task<RoomStatus?> GetRoomStatus(Expression<Func<RoomStatus, bool>> expression)
    {
        return await _unitOfWork.RoomStatuses.GetEntity(expression);
    }

    public async Task<IEnumerable<RoomStatus>?> GetRoomStatuses()
    {
        return await _unitOfWork.RoomStatuses.GetEntities(roomStat => true);
    }

    public async Task<IEnumerable<RoomStatus>?> GetRoomStatuses(Expression<Func<RoomStatus, bool>> expression)
    {
        return await _unitOfWork.RoomStatuses.GetEntities(expression);
    }

    public async Task<int> DeleteRoomStatus(int id)
    {
        await _unitOfWork.RoomStatuses.Delete(id);
        return await _unitOfWork.SaveChanges();
    }

    public async Task UpdateRoomStatus(RoomStatus roomStatus)
    {
        await _unitOfWork.RoomStatuses.Update(roomStatus);
        await _unitOfWork.SaveChanges();
    }

    public async Task<RoomStatus> PostRoomStatus(RoomStatus roomStatus)
    {
        var added = await _unitOfWork.RoomStatuses.Add(roomStatus);
        await _unitOfWork.SaveChanges();
        return added;
    }
}