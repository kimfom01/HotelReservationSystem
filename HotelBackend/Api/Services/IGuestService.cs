using System.Linq.Expressions;
using DataAccess.Models;

namespace Api.Services;

public interface IGuestService
{
    Task<Guest?> GetGuest(int id);
    Task<Guest?> GetGuest(Expression<Func<Guest, bool>> expression);
    Task<IEnumerable<Guest>?> GetGuests();
    Task<IEnumerable<Guest>?> GetGuests(Expression<Func<Guest, bool>> expression);
    Task<int> DeleteGuest(int id);
    Task UpdateGuest(Guest guest);
    Task<Guest> PostGuest(Guest guest);
}