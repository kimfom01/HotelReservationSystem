using Api.Dtos;
using Api.Services;
using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelAmenityController : ControllerBase
{
    private readonly IDataServiceGeneric<HotelAmenity> _dataService;
    private readonly IMapper _mapper;

    public HotelAmenityController(IDataServiceGeneric<HotelAmenity> dataService,
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

        var amenityDto = _mapper.Map<HotelAmenityDto>(amenity);

        return Ok(amenityDto);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetAmenities()
    {
        var amenities = await _dataService.GetEntities();

        var amenitiesDtos = _mapper.Map<IEnumerable<HotelAmenityDto>>(amenities);

        return Ok(amenitiesDtos);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteAmenity(int id)
    {
        var deletedCount = await _dataService.DeleteEntity(id);

        if (deletedCount <= 0)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateAmenity(HotelAmenityDto hotelAmenityDto)
    {
        var amenity = _mapper.Map<HotelAmenity>(hotelAmenityDto);

        await _dataService.UpdateEntity(amenity);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostAmenity(HotelAmenityDto hotelAmenityDto)
    {
        var amenity = _mapper.Map<HotelAmenity>(hotelAmenityDto);

        var added = await _dataService.PostEntity(amenity);

        var amenityResult = _mapper.Map<HotelAmenityDto>(added);

        return CreatedAtAction(nameof(GetAmenity), new { id = amenityResult.Id }, amenityResult);
    }
}
