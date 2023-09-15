using DataAccess.Models;

namespace Api.Services;

public class ReservationService : IReservationService
{
    private readonly IRoomService _roomService;
    private readonly IDataServiceGeneric<Reservation> _dataService;
    private readonly IReservationRoomService _reservationRoomService;

    public ReservationService(IRoomService roomService,
        IDataServiceGeneric<Reservation> dataService,
        IReservationRoomService reservationRoomService)
    {
        _roomService = roomService;
        _dataService = dataService;
        _reservationRoomService = reservationRoomService;
    }

    public async Task<Reservation?> MakeReservation(Reservation reservation)
    {
        var availableRooms = await _roomService
            .GetAvailableRoomsPerRoomCapacity(reservation.HotelId, reservation.NumberOfGuests);

        if (availableRooms is null || !availableRooms.Any())
        {
            return null;
        }

        var result = await _dataService.PostEntity(reservation);

        var room = availableRooms.First();
        room.Availabilty = false;
        await _roomService.UpdateRoom(room);

        var reservationRoom = new ReservationRoom
        {
            ReservationId = reservation.Id,
            RoomId = room.Id
        };

        await _reservationRoomService.AddReservationRoom(reservationRoom);

        return result;
    }
}