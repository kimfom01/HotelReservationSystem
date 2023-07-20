using HotelManagement.Models;
using HotelManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MaintenanceController : ControllerBase
{
    private readonly IRepository<Maintenance> _repository;

    public MaintenanceController(IRepository<Maintenance> repository)
    {
        _repository = repository;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetMaintenance(int id)
    {
        var maintenance = await _repository.GetEntity(maintenance => maintenance.Id == id);

        if (maintenance is null)
        {
            return NotFound();
        }

        return Ok(maintenance);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetMaintenances()
    {
        var maintenances = await _repository.GetEntities(maintenance => true);

        return Ok(maintenances);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteMaintenance(int id)
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
    public async Task<IActionResult> UpdateMaintenance(Maintenance maintenance)
    {
        await _repository.Update(maintenance);
        await _repository.SaveChanges();

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostMaintenance(Maintenance maintenance)
    {
        var added = await _repository.Add(maintenance);
        await _repository.SaveChanges();

        return CreatedAtAction(nameof(GetMaintenance), new { id = added.Id }, added);
    }
}
