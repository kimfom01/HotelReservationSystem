using System.Linq.Expressions;
using HotelBackend.ReservationService.Models;
using HotelBackend.ReservationService.Repositories;

namespace HotelBackend.ReservationService.Services.Implementations;

public class RoomService : IRoomService
{
    private readonly IUnitOfWork _unitOfWork;

    public RoomService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> DeleteRoom(Guid id)
    {
        await _unitOfWork.Rooms.Delete(id);
        return await _unitOfWork.SaveChanges();
    }

    public async Task<IEnumerable<Room>> GetAvailableRoomsPerRoomCapacity(Guid hotelId, int capacity)
    {
        var rooms = await _unitOfWork.Rooms
            .GetEntities(room => room.HotelId == hotelId
                                 && room.Availability == true);

        return rooms;
    }

    public async Task<Room?> GetRoom(Guid id)
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

    public async Task<Room?> PostRoom(Room room)
    {
        var added = await _unitOfWork.Rooms.Add(room);
        await _unitOfWork.SaveChanges();

        return added;
    }

    public async Task UpdateRoom(Room room)
    {
        await _unitOfWork.Rooms.Update(room);
        await _unitOfWork.SaveChanges();
    }
}