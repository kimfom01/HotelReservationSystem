using Api.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MaintenanceController : ControllerBase
{
    private readonly IDataServiceGeneric<Maintenance> _dataService;

    public MaintenanceController(IDataServiceGeneric<Maintenance> dataService)
    {
        _dataService = dataService;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetMaintenance(int id)
    {
        var maintenance = await _dataService.GetEntity(id);

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
        var maintenances = await _dataService.GetEntities();

        return Ok(maintenances);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteMaintenance(int id)
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
    public async Task<IActionResult> UpdateMaintenance(Maintenance maintenance)
    {
        await _dataService.UpdateEntity(maintenance);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostMaintenance(Maintenance maintenance)
    {
        var added = await _dataService.PostEntity(maintenance);

        return CreatedAtAction(nameof(GetMaintenance), new { id = added.Id }, added);
    }
}
