namespace HotelBackend.ReservationService.Room;

public interface IRoomService
{
    Task<IEnumerable<RoomDto>> GetAvailableRoomsPerRoomCapacity(Guid hotelId);
    Task<RoomModel?> GetRoom(Guid id);
}
