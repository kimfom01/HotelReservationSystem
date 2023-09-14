using DataAccess.Models;

namespace Api.Services;

public interface IReservationRoomService
{
    Task<ReservationRoom?> AddReservationRoom(ReservationRoom reservationRoom);
}
