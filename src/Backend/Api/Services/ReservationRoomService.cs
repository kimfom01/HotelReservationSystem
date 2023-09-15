using DataAccess.Models;

namespace Api.Services;

public class ReservationRoomService : IReservationRoomService
{
    private readonly IDataServiceGeneric<ReservationRoom> _dataService;

    public ReservationRoomService(IDataServiceGeneric<ReservationRoom> dataService)
    {
        _dataService = dataService;
    }

    public async Task<ReservationRoom?> AddReservationRoom(ReservationRoom reservationRoom)
    {
        var added = await _dataService.PostEntity(reservationRoom);

        return added;
    }
}