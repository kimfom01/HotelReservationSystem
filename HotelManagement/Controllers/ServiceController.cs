using HotelManagement.Models;
using HotelManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceController : ControllerBase
{
    private readonly IRepository<Service> _repository;

    public ServiceController(IRepository<Service> repository)
    {
        _repository = repository;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetService(int id)
    {
        var service = await _repository.GetEntity(service => service.Id == id);

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
        var services = await _repository.GetEntities(service => true);

        return Ok(services);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteService(int id)
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
    public async Task<IActionResult> UpdateService(Service service)
    {
        await _repository.Update(service);
        await _repository.SaveChanges();

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostService(Service service)
    {
        var added = await _repository.Add(service);
        await _repository.SaveChanges();

        return CreatedAtAction(nameof(GetService), new { id = added.Id }, added);
    }
}
