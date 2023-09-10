using Api.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MealController : ControllerBase
{
    private readonly IDataServiceGeneric<Meal> _dataService;

    public MealController(IDataServiceGeneric<Meal> dataService)
    {
        _dataService = dataService;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetMeal(int id)
    {
        var meal = await _dataService.GetEntity(id);

        if (meal is null)
        {
            return NotFound();
        }

        return Ok(meal);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetMeals()
    {
        var meals = await _dataService.GetEntities();

        return Ok(meals);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteMeal(int id)
    {
        int deletedCount = await _dataService.DeleteEntity(id);

        if (deletedCount <= 0)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateMeal(Meal meal)
    {
        await _dataService.UpdateEntity(meal);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostMeal(Meal meal)
    {
        var added = await _dataService.PostEntity(meal);

        return CreatedAtAction(nameof(GetMeal), new { id = added.Id }, added);
    }
}
