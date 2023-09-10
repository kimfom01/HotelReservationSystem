using Api.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceController : ControllerBase
{
    private readonly IDataServiceGeneric<Service> _dataService;

    public ServiceController(IDataServiceGeneric<Service> dataService)
    {
        _dataService = dataService;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetService(int id)
    {
        var service = await _dataService.GetEntity(service => service.Id == id);

        if (service is null)
        {
            return NotFound();
        }

        return Ok(service);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetServices()
    {
        var services = await _dataService.GetEntities();

        return Ok(services);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteService(int id)
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
    public async Task<IActionResult> UpdateService(Service service)
    {
        await _dataService.UpdateEntity(service);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostService(Service service)
    {
        var added = await _dataService.PostEntity(service);

        return CreatedAtAction(nameof(GetService), new { id = added.Id }, added);
    }
}
