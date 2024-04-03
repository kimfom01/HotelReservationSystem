using System.Linq.Expressions;
using HotelBackend.Old.Remove.DataAccess.Models;

namespace HotelBackend.Old.Remove.Api.Services;

public interface IRoomStatusService
{
    public Task<RoomStatus?> GetRoomStatus(int id);
    public Task<RoomStatus?> GetRoomStatus(Expression<Func<RoomStatus, bool>> expression);
    public Task<IEnumerable<RoomStatus>?> GetRoomStatuses();
    public Task<IEnumerable<RoomStatus>?> GetRoomStatuses(Expression<Func<RoomStatus, bool>> expression);
    public Task<int> DeleteRoomStatus(int id);
    public Task UpdateRoomStatus(RoomStatus roomStatus);
    public Task<RoomStatus> PostRoomStatus(RoomStatus roomStatus);
}