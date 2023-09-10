using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Models.Dtos;
using Web.Models.ViewModels;
using Web.Services;

namespace HotelManagement.Web.Controllers;

public class MealController : Controller
{
    private readonly IGenericApiService<Meal> _mealService;
    private readonly IGenericApiService<Hotel> _hotelService;

    public MealController(
        IGenericApiService<Meal> mealService,
        IGenericApiService<Hotel> hotelService)
    {
        _mealService = mealService;
        _hotelService = hotelService;
    }

    public async Task<IActionResult> Index()
    {
        var meals = await _mealService.FetchEntities();

        var mealViewModels = new List<MealViewModel>();

        foreach (var meal in meals)
        {
            var hotel = await _hotelService.FetchEntity(meal.HotelId);

            var mealVM = new MealViewModel
            {
                HotelName = hotel.Name,
                Name = meal.Name,
                Type = meal.Type,
                MealPrice = meal.MealPrice,
                MealId = meal.Id
            };

            mealViewModels.Add(mealVM);
        }

        return View(mealViewModels);
    }

    public async Task<IActionResult> AddMeal()
    {
        var hotels = await _hotelService.FetchEntities();

        var mealViewModel = new MealViewModel
        {
            Hotels = new SelectList(hotels, "Id", "Name")
        };

        return View(mealViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddMeal(MealViewModel mealViewModel)
    {
        var meal = new Meal
        {
            Name = mealViewModel.Name,
            Type = mealViewModel.Type,
            MealPrice = mealViewModel.MealPrice,
            HotelId = mealViewModel.HotelId,
        };

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

        var hotels = await _hotelService.FetchEntities();

        var mealViewModel = new MealViewModel
        {
            Name = meal.Name,
            Type = meal.Type,
            MealPrice = meal.MealPrice,
            HotelId = meal.HotelId,
            Hotels = new SelectList(hotels, "Id", "Name")
        };

        return View(mealViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> ManageMeal(MealViewModel newMealInfo, int mealId)
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
