using HotelBackend.ReservationService.Dtos;
using HotelBackend.ReservationService.Models;

namespace HotelBackend.ReservationService.Services;

public interface IReservationService
{
    Task<ReservationDto> MakeReservation(ReservationDto reservationDto);
    Task<Reservation?> GetReservation(Guid id);
    Task<IEnumerable<Reservation>?> GetReservations();
}
