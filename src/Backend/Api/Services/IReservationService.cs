using DataAccess.Models;

namespace Api.Services;

public interface IReservationService
{
    Task<Reservation?> MakeReservation(Reservation reservation);
}
