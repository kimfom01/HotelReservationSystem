using System.Linq.Expressions;
using DataAccess.Models;

namespace HotelBackend.General.Api.Services;

public interface IReservationService
{
    Task<Reservation?> MakeReservation(Reservation reservation);
    Task<Reservation?> GetReservation(int id);
    Task<IEnumerable<Reservation>?> GetReservations();
    Task<int> DeleteReservation(int id);
    Task UpdateReservation(Reservation reservation);
}
