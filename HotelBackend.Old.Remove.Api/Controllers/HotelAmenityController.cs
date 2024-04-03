using AutoMapper;
using HotelBackend.Old.Remove.DataAccess.Models;
using HotelBackend.Old.Remove.Api.Dtos;
using HotelBackend.Old.Remove.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Old.Remove.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelAmenityController : ControllerBase
{
    private readonly IHotelAmenityService _hotelAmenityService;
    private readonly IMapper _mapper;

    public HotelAmenityController(
        IHotelAmenityService hotelAmenityService,
        IMapper mapper)
    {
        _hotelAmenityService = hotelAmenityService;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAmenity(int id)
    {
        var amenity = await _hotelAmenityService.GetHotelAmenity(id);

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
        var amenities = await _hotelAmenityService.GetHotelAmenities();

        var amenitiesDtos = _mapper.Map<IEnumerable<HotelAmenityDto>>(amenities);

        return Ok(amenitiesDtos);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteAmenity(int id)
    {
        var deletedCount = await _hotelAmenityService.DeleteHotelAmenity(id);

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

        await _hotelAmenityService.UpdateHotelAmenity(amenity);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostAmenity(HotelAmenityDto hotelAmenityDto)
    {
        var amenity = _mapper.Map<HotelAmenity>(hotelAmenityDto);

        var added = await _hotelAmenityService.PostHotelAmenity(amenity);

        var amenityResult = _mapper.Map<HotelAmenityDto>(added);

        return CreatedAtAction(nameof(GetAmenity), new { id = amenityResult.Id }, amenityResult);
    }
}
