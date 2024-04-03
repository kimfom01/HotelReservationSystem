using HotelBackend.Old.Remove.DataAccess.Data;
using HotelBackend.Old.Remove.DataAccess.Models;

namespace HotelBackend.Old.Remove.DataAccess.Repositories.Implementations;

public class MealRepository : Repository<Meal>, IMealRepository
{
    public MealRepository(Context context) : base(context)
    {
    }
}