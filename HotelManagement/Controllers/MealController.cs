using HotelManagement.Models;
using HotelManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MealController : ControllerBase
{
    private readonly IRepository<Meal> _repository;

    public MealController(IRepository<Meal> repository)
    {
        _repository = repository;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetMeal(int id)
    {
        var meal = await _repository.GetEntity(meal => meal.Id == id);

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
        var meals = await _repository.GetEntities(meal => true);

        return Ok(meals);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteMeal(int id)
    {
        await _repository.Delete(id);
        int deltedCount = await _repository.SaveChanges();

        if (deltedCount <= 0)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateMeal(Meal meal)
    {
        await _repository.Update(meal);
        await _repository.SaveChanges();

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostMeal(Meal meal)
    {
        var added = await _repository.Add(meal);
        await _repository.SaveChanges();

        return CreatedAtAction(nameof(GetMeal), new { id = added.Id }, added);
    }
}
