namespace Api.Services;

public interface IRoomService
{
    Task<bool> CheckIfRoomAvailable();
}

public class RoomService : IRoomService
{


    public Task<bool> CheckIfRoomAvailable()
    {
        throw new NotImplementedException();
    }
}