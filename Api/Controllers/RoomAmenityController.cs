using Api.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomAmenityController : ControllerBase
{
    private readonly IDataServiceGeneric<RoomAmenity> _dataService;

    public RoomAmenityController(IDataServiceGeneric<RoomAmenity> dataService)
    {
        _dataService = dataService;
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

        return Ok(amenity);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetAmenities()
    {
        var amenities = await _dataService.GetEntities();

        return Ok(amenities);
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
    public async Task<IActionResult> UpdateAmenity(RoomAmenity amenity)
    {
        await _dataService.UpdateEntity(amenity);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostHotel(RoomAmenity amenity)
    {
        var added = await _dataService.PostEntity(amenity);

        return CreatedAtAction(nameof(GetAmenity), new { id = added.Id }, added);
    }
}
