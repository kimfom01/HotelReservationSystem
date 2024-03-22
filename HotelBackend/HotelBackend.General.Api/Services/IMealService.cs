using System.Linq.Expressions;
using DataAccess.Models;

namespace HotelBackend.General.Api.Services;

public interface IMealService
{
    public Task<Meal?> GetMeal(int id);
    public Task<Meal?> GetMeal(Expression<Func<Meal, bool>> expression);
    public Task<IEnumerable<Meal>?> GetMeals();
    public Task<IEnumerable<Meal>?> GetMeals(Expression<Func<Meal, bool>> expression);
    public Task<int> DeleteMeal(int id);
    public Task UpdateMeal(Meal meal);
    public Task<Meal> PostMeal(Meal meal);
}