using System.Linq.Expressions;
using DataAccess.Models;
using DataAccess.Repositories;

namespace Api.Services.Implementations;

public class RoomService : IRoomService
{
    private readonly IUnitOfWork _unitOfWork;

    public RoomService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> DeleteRoom(int id)
    {
        await _unitOfWork.Rooms.Delete(id);
        return await _unitOfWork.SaveChanges();
    }

    public async Task<IEnumerable<Room>?> GetAvailableRoomsPerRoomCapacity(int hotelId, int capacity)
    {
        var rooms = await _unitOfWork.Rooms.GetEntities(room => room.Capacity == capacity
                && room.HotelId == hotelId
                && room.Availabilty == true);

        return rooms;
    }

    public async Task<Room?> GetRoom(int id)
    {
        return await _unitOfWork.Rooms.GetEntity(id);
    }

    public async Task<Room?> GetRoom(Expression<Func<Room, bool>> expression)
    {

        return await _unitOfWork.Rooms.GetEntity(expression);
    }

    public async Task<IEnumerable<Room>?> GetRooms()
    {
        return await _unitOfWork.Rooms.GetEntities(ro => true);
    }

    public async Task<Room> PostRoom(Room room)
    {
        var added = await _unitOfWork.Rooms.Add(room);
        await _unitOfWork.SaveChanges();

        return added;
    }

    public async Task UpdateRoom(Room room)
    {
        await _unitOfWork.Rooms.Update(room);
    }
}