using HotelManagement.Web.Models.Dtos;
using HotelManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Web.Controllers;

public class MealController : Controller
{
    private readonly IGenericApiService<Meal> _mealService;

    public MealController(IGenericApiService<Meal> mealService)
    {
        _mealService = mealService;
    }

    public async Task<IActionResult> Index()
    {
        var meals = await _mealService.FetchEntities();

        return View(meals);
    }

    public IActionResult AddMeal()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddMeal(Meal meal)
    {
        var addedMeal = await _mealService.AddEntity(meal);

        if (addedMeal is null)
        {
            return View();
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ManageMeal(int mealId)
    {
        var meal = await _mealService.FetchEntity(mealId);

        if (meal is null)
        {
            return NotFound($"Meal with id = {mealId} cannot be found");
        }

        return View(meal);
    }

    [HttpPost]
    public async Task<IActionResult> ManageMeal(Meal newMealInfo, int mealId)
    {
        var meal = await _mealService.FetchEntity(mealId);

        if (meal is null)
        {
            return View();
        }

        meal.Name = newMealInfo.Name;
        meal.Type = newMealInfo.Type;
        meal.MealPrice = newMealInfo.MealPrice;
        meal.HotelId = newMealInfo.HotelId;

        var success = await _mealService.UpdateEntity(meal);

        if (!success)
        {
            return View();
        }

        return RedirectToAction(nameof(Index));
    }
}
