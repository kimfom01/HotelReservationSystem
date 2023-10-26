using System.Linq.Expressions;
using DataAccess.Models;
using DataAccess.Repositories;

namespace Api.Services.Implementations;

public class GuestService : IGuestService
{
    private readonly IUnitOfWork _unitOfWork;

    public GuestService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<int> DeleteGuest(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Guest?> GetGuest(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Guest?> GetGuest(Expression<Func<Guest, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Guest>?> GetGuests()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Guest>?> GetGuests(Expression<Func<Guest, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<Guest> PostGuest(Guest guest)
    {
        throw new NotImplementedException();
    }

    public Task UpdateGuest(Guest guest)
    {
        throw new NotImplementedException();
    }
}
