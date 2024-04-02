using System.Linq.Expressions;
using DataAccess.Models;
using DataAccess.Repositories;

namespace HotelBackend.General.Api.Services.Implementations;

public class GuestService : IGuestService
{
    private readonly IUnitOfWork _unitOfWork;

    public GuestService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> DeleteGuest(int id)
    {
        await _unitOfWork.Guests.Delete(id);
        return await _unitOfWork.SaveChanges();
    }

    public async Task<Guest?> GetGuest(int id)
    {
        return await _unitOfWork.Guests.GetEntity(id);
    }

    public async Task<Guest?> GetGuest(Expression<Func<Guest, bool>> expression)
    {
        return await _unitOfWork.Guests.GetEntity(expression);
    }

    public async Task<IEnumerable<Guest>?> GetGuests()
    {
        return await _unitOfWork.Guests.GetEntities(guest => true);
    }

    public async Task<IEnumerable<Guest>?> GetGuests(Expression<Func<Guest, bool>> expression)
    {
        return await _unitOfWork.Guests.GetEntities(expression);
    }

    public async Task<Guest> PostGuest(Guest guest)
    {
        var added = await _unitOfWork.Guests.Add(guest);
        await _unitOfWork.SaveChanges();
        return added;
    }

    public async Task UpdateGuest(Guest guest)
    {
        await _unitOfWork.Guests.Update(guest);
        await _unitOfWork.SaveChanges();
    }
}
