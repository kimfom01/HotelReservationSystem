using AutoMapper;
using HotelBackend.Old.Remove.DataAccess.Models;
using HotelBackend.Old.Remove.Api.Dtos;
using HotelBackend.Old.Remove.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Old.Remove.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceController : ControllerBase
{
    private readonly IServiceService _serviceService;
    private readonly IMapper _mapper;

    public ServiceController(
        IMapper mapper, 
        IServiceService serviceService
        )
    {
        _mapper = mapper;
        _serviceService = serviceService;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetService(int id)
    {
        var service = await _serviceService.GetService(id);

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
        var services = await _serviceService.GetServices();

        var servicesDtos = _mapper.Map<IEnumerable<ServiceDto>>(services);

        return Ok(servicesDtos);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteService(int id)
    {
        int deletedCount = await _serviceService.DeleteService(id);

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

        await _serviceService.UpdateService(service);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostService(ServiceDto serviceDto)
    {
        var service = _mapper.Map<Service>(serviceDto);

        var added = await _serviceService.PostService(service);

        var serviceDtoResult = _mapper.Map<ServiceDto>(added);

        return CreatedAtAction(nameof(GetService), new { id = serviceDtoResult.Id }, serviceDtoResult);
    }
}
