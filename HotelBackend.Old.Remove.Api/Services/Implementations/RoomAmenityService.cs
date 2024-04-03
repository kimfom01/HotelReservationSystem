using System.Linq.Expressions;
using HotelBackend.Old.Remove.DataAccess.Models;
using HotelBackend.Old.Remove.DataAccess.Repositories;

namespace HotelBackend.Old.Remove.Api.Services.Implementations;

public class RoomAmenityService : IRoomAmenityService
{
    private readonly IUnitOfWork _unitOfWork;

    public RoomAmenityService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<RoomAmenity?> GetRoomAmenity(int id)
    {
        return await _unitOfWork.RoomAmenities.GetEntity(id);
    }

    public async Task<RoomAmenity?> GetRoomAmenity(Expression<Func<RoomAmenity, bool>> expression)
    {
        return await _unitOfWork.RoomAmenities.GetEntity(expression);
    }

    public async Task<IEnumerable<RoomAmenity>?> GetRoomAmenities()
    {
        return await _unitOfWork.RoomAmenities.GetEntities(roomAmen => true);
    }

    public async Task<IEnumerable<RoomAmenity>?> GetRoomAmenities(Expression<Func<RoomAmenity, bool>> expression)
    {
        return await _unitOfWork.RoomAmenities.GetEntities(expression);
    }

    public async Task<int> DeleteRoomAmenity(int id)
    {
        await _unitOfWork.RoomAmenities.Delete(id);
        return await _unitOfWork.SaveChanges();
    }

    public async Task UpdateRoomAmenity(RoomAmenity roomAmenity)
    {
        await _unitOfWork.RoomAmenities.Update(roomAmenity);
        await _unitOfWork.SaveChanges();
    }

    public async Task<RoomAmenity> PostRoomAmenity(RoomAmenity roomAmenity)
    {
        var added = await _unitOfWork.RoomAmenities.Add(roomAmenity);
        await _unitOfWork.SaveChanges();
        return added;
    }
}