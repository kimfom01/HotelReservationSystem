using Api.Dtos;
using Api.Services;
using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MealController : ControllerBase
{
    private readonly IMealService _mealService;
    private readonly IMapper _mapper;

    public MealController(
        IMealService mealService,
        IMapper mapper)
    {
        _mealService = mealService;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetMeal(int id)
    {
        var meal = await _mealService.GetMeal(id);

        if (meal is null)
        {
            return NotFound();
        }

        var mealDto = _mapper.Map<MealDto>(meal);

        return Ok(mealDto);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetMeals()
    {
        var meals = await _mealService.GetMeals();

        var mealsDtos = _mapper.Map<IEnumerable<MealDto>>(meals);

        return Ok(mealsDtos);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteMeal(int id)
    {
        int deletedCount = await _mealService.DeleteMeal(id);

        if (deletedCount <= 0)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateMeal(MealDto mealDto)
    {
        var meal = _mapper.Map<Meal>(mealDto);

        await _mealService.UpdateMeal(meal);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostMeal(MealDto mealDto)
    {
        var meal = _mapper.Map<Meal>(mealDto);

        var added = await _mealService.PostMeal(meal);

        var mealResult = _mapper.Map<MealDto>(added);

        return CreatedAtAction(nameof(GetMeal), new { id = mealResult.Id }, mealResult);
    }
}
