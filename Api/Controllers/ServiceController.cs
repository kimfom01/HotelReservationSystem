using Api.Dtos;
using Api.Services;
using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceController : ControllerBase
{
    private readonly IDataServiceGeneric<Service> _dataService;
    private readonly IMapper _mapper;

    public ServiceController(IDataServiceGeneric<Service> dataService,
        IMapper mapper)
    {
        _dataService = dataService;
        _mapper = mapper;
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

        var serviceDto = _mapper.Map<ServiceDto>(service);

        return Ok(serviceDto);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetServices()
    {
        var services = await _dataService.GetEntities();

        var servicesDtos = _mapper.Map<IEnumerable<ServiceDto>>(services);

        return Ok(servicesDtos);
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
    public async Task<IActionResult> UpdateService(ServiceDto serviceDto)
    {
        var service = _mapper.Map<Service>(serviceDto);

        await _dataService.UpdateEntity(service);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostService(ServiceDto serviceDto)
    {
        var service = _mapper.Map<Service>(serviceDto);

        var added = await _dataService.PostEntity(service);

        var serviceDtoResult = _mapper.Map<ServiceDto>(added);

        return CreatedAtAction(nameof(GetService), new { id = serviceDtoResult.Id }, serviceDtoResult);
    }
}
