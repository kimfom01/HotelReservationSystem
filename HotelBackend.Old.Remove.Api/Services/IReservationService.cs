using System.Linq.Expressions;
using HotelBackend.Old.Remove.DataAccess.Models;

namespace HotelBackend.Old.Remove.Api.Services;

public interface IReservationService
{
    Task<Reservation?> MakeReservation(Reservation reservation);
    Task<Reservation?> GetReservation(int id);
    Task<IEnumerable<Reservation>?> GetReservations();
    Task<int> DeleteReservation(int id);
    Task UpdateReservation(Reservation reservation);
}
