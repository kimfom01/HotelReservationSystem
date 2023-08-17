using HotelManagement.Models;
using HotelManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OccupancyController : ControllerBase
{
    private readonly IRepository<Occupancy> _repository;

    public OccupancyController(IRepository<Occupancy> repository)
    {
        _repository = repository;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetOccupancy(int id)
    {
        var occupancy = await _repository.GetEntity(occ => occ.Id == id);

        if (occupancy is null)
        {
            return NotFound();
        }

        return Ok(occupancy);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetOccupancies()
    {
        var occupancies = await _repository.GetEntities(occ => true);

        return Ok(occupancies);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteOccupancy(int id)
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
    public async Task<IActionResult> UpdateOccupancy(Occupancy occupancy)
    {
        await _repository.Update(occupancy);
        await _repository.SaveChanges();

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostOccupancy(Occupancy occupancy)
    {
        var added = await _repository.Add(occupancy);
        await _repository.SaveChanges();

        return CreatedAtAction(nameof(GetOccupancy), new { id = added.Id }, added);
    }
}
