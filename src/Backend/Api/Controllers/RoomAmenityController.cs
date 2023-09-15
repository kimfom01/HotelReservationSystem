using Api.Dtos;
using Api.Services;
using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomAmenityController : ControllerBase
{
    private readonly IDataServiceGeneric<RoomAmenity> _dataService;
    private readonly IMapper _mapper;

    public RoomAmenityController(IDataServiceGeneric<RoomAmenity> dataService,
        IMapper mapper)
    {
        _dataService = dataService;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAmenity(int id)
    {
        var amenity = await _dataService.GetEntity(id);

        if (amenity is null)
        {
            return NotFound();
        }

        var amenityDto = _mapper.Map<RoomAmenityDto>(amenity);

        return Ok(amenityDto);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetAmenities()
    {
        var amenities = await _dataService.GetEntities();

        var amenitiesDtos = _mapper.Map<IEnumerable<RoomAmenityDto>>(amenities);

        return Ok(amenitiesDtos);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteAmenity(int id)
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
    public async Task<IActionResult> UpdateAmenity(RoomAmenityDto amenityDto)
    {
        var amenity = _mapper.Map<RoomAmenity>(amenityDto);

        await _dataService.UpdateEntity(amenity);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostHotel(RoomAmenityDto amenityDto)
    {
        var amenity = _mapper.Map<RoomAmenity>(amenityDto);

        var added = await _dataService.PostEntity(amenity);

        var amenityResult = _mapper.Map<RoomAmenityDto>(added);

        return CreatedAtAction(nameof(GetAmenity), new { id = amenityResult.Id }, amenityResult);
    }
}
