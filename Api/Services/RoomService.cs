using DataAccess.Models;

namespace Api.Services;

public class RoomService : IRoomService
{
    private readonly IDataServiceGeneric<Room> _dataService;

    public RoomService(IDataServiceGeneric<Room> dataService)
    {
        _dataService = dataService;
    }

    public async Task<IEnumerable<Room>?> GetAvailableRoomsPerRoomCapacity(int hotelId, int capacity)
    {
        var rooms = await _dataService
            .GetEntities(room => room.Capacity == capacity
                && room.HotelId == hotelId
                && room.Availabilty == true);

        return rooms;
    }

    public async Task UpdateRoom(Room room)
    {
        await _dataService.UpdateEntity(room);
    }
}