using DataAccess.Data;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementations;

public class MealRepository : Repository<Meal>, IMealRepository
{
    public MealRepository(Context context) : base(context)
    {
    }
}