using DataAccess.Models;
using HotelManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelAmenityController : ControllerBase
{
    private readonly IDataServiceGeneric<HotelAmenity> _dataService;

    public HotelAmenityController(IDataServiceGeneric<HotelAmenity> dataService)
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
        var deletedCount = await _dataService.DeleteEntity(id);

        if (deletedCount <= 0)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateAmenity(HotelAmenity amenity)
    {
        await _dataService.UpdateEntity(amenity);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostAmenity(HotelAmenity amenity)
    {
        var added = await _dataService.PostEntity(amenity);

        return CreatedAtAction(nameof(GetAmenity), new { id = added.Id }, added);
    }
}
