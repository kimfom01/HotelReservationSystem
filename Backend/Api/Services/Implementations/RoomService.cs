using System.Linq.Expressions;
using DataAccess.Models;
using DataAccess.Repositories;

namespace Api.Services.Implementations;

public class RoomService : IRoomService
{
    private readonly IDataServiceGeneric<Room> _dataService;
    private readonly IUnitOfWork _unitOfWork;

    public RoomService(IDataServiceGeneric<Room> dataService, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _dataService = dataService;
    }

    public Task<int> DeleteRoom(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Room>?> GetAvailableRoomsPerRoomCapacity(int hotelId, int capacity)
    {
        var rooms = await _unitOfWork.Rooms.GetEntities(room => room.Capacity == capacity
                && room.HotelId == hotelId
                && room.Availabilty == true);

        return rooms;
    }

    public Task<Room?> GetRoom(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Room?> GetRoom(Expression<Func<Room, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Room>?> GetRooms()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Room>?> GetRooms(Expression<Func<Room, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<Room> PostRoom(Room room)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateRoom(Room room)
    {
        await _unitOfWork.Rooms.Update(room);
    }
}