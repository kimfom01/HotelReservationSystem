using System.Linq.Expressions;
using DataAccess.Models;
using DataAccess.Repositories;

namespace HotelBackend.General.Api.Services.Implementations;

public class MealService : IMealService
{
    private readonly IUnitOfWork _unitOfWork;

    public MealService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Meal?> GetMeal(int id)
    {
        return await _unitOfWork.Meals.GetEntity(id);
    }

    public async Task<Meal?> GetMeal(Expression<Func<Meal, bool>> expression)
    {
        return await _unitOfWork.Meals.GetEntity(expression);
    }

    public async Task<IEnumerable<Meal>?> GetMeals()
    {
        return await _unitOfWork.Meals.GetEntities(meal => true);
    }

    public async Task<IEnumerable<Meal>?> GetMeals(Expression<Func<Meal, bool>> expression)
    {
        return await _unitOfWork.Meals.GetEntities(expression);
    }

    public async Task<int> DeleteMeal(int id)
    {
        await _unitOfWork.Meals.Delete(id);
        return await _unitOfWork.SaveChanges();
    }

    public async Task UpdateMeal(Meal meal)
    {
        await _unitOfWork.Meals.Update(meal);
    }

    public async Task<Meal> PostMeal(Meal meal)
    {
        var added = await _unitOfWork.Meals.Add(meal);
        await _unitOfWork.SaveChanges();
        return added;
    }
}