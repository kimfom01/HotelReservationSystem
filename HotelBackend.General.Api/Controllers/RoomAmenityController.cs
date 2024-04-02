using AutoMapper;
using DataAccess.Models;
using HotelBackend.General.Api.Dtos;
using HotelBackend.General.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.General.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomAmenityController : ControllerBase
{
    private readonly IRoomAmenityService _roomAmenityService;
    private readonly IMapper _mapper;

    public RoomAmenityController(
        IRoomAmenityService roomAmenityService,
        IMapper mapper)
    {
        _roomAmenityService = roomAmenityService;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAmenity(int id)
    {
        var amenity = await _roomAmenityService.GetRoomAmenity(id);

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
        var amenities = await _roomAmenityService.GetRoomAmenities();

        var amenitiesDtos = _mapper.Map<IEnumerable<RoomAmenityDto>>(amenities);

        return Ok(amenitiesDtos);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteAmenity(int id)
    {
        int deletedCount = await _roomAmenityService.DeleteRoomAmenity(id);

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

        await _roomAmenityService.UpdateRoomAmenity(amenity);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostHotel(RoomAmenityDto amenityDto)
    {
        var amenity = _mapper.Map<RoomAmenity>(amenityDto);

        var added = await _roomAmenityService.PostRoomAmenity(amenity);

        var amenityResult = _mapper.Map<RoomAmenityDto>(added);

        return CreatedAtAction(nameof(GetAmenity), new { id = amenityResult.Id }, amenityResult);
    }
}