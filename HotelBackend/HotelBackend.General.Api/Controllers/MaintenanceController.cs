using AutoMapper;
using DataAccess.Models;
using HotelBackend.General.Api.Dtos;
using HotelBackend.General.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.General.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MaintenanceController : ControllerBase
{
    private readonly IMaintenanceService _maintenanceService;
    private readonly IMapper _mapper;

    public MaintenanceController(
        IMaintenanceService maintenanceService,
        IMapper mapper)
    {
        _maintenanceService = maintenanceService;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetMaintenance(int id)
    {
        var maintenance = await _maintenanceService.GetMaintenance(id);

        if (maintenance is null)
        {
            return NotFound();
        }

        var maintenanceDto = _mapper.Map<MaintenanceDto>(maintenance);

        return Ok(maintenanceDto);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetMaintenances()
    {
        var maintenances = await _maintenanceService.GetMaintenances();

        var maintenancesDtos = _mapper.Map<IEnumerable<MaintenanceDto>>(maintenances);

        return Ok(maintenancesDtos);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteMaintenance(int id)
    {
        int deletedCount = await _maintenanceService.DeleteMaintenance(id);

        if (deletedCount <= 0)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateMaintenance(MaintenanceDto maintenanceDto)
    {
        var maintenance = _mapper.Map<Maintenance>(maintenanceDto);

        await _maintenanceService.UpdateMaintenance(maintenance);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostMaintenance(MaintenanceDto maintenanceDto)
    {
        var maintenance = _mapper.Map<Maintenance>(maintenanceDto);

        var added = await _maintenanceService.PostMaintenance(maintenance);

        var maintenanceResult = _mapper.Map<MaintenanceDto>(added);

        return CreatedAtAction(nameof(GetMaintenance), new { id = maintenanceResult.Id }, maintenanceResult);
    }
}
