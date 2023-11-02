using System.Linq.Expressions;
using DataAccess.Models;

namespace Api.Services;

public interface IRoomAmenityService
{
    public Task<RoomAmenity?> GetRoomAmenity(int id);
    public Task<RoomAmenity?> GetRoomAmenity(Expression<Func<RoomAmenity, bool>> expression);
    public Task<IEnumerable<RoomAmenity>?> GetRoomAmenities();
    public Task<IEnumerable<RoomAmenity>?> GetRoomAmenities(Expression<Func<RoomAmenity, bool>> expression);
    public Task<int> DeleteRoomAmenity(int id);
    public Task UpdateRoomAmenity(RoomAmenity roomAmenity);
    public Task<RoomAmenity> PostRoomAmenity(RoomAmenity roomAmenity);
}