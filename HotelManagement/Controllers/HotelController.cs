using DataAccess.Models;
using HotelManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelController : ControllerBase
{
    private readonly IDataServiceGeneric<Hotel> _dataService;

    public HotelController(IDataServiceGeneric<Hotel> dataService)
    {
        _dataService = dataService;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetHotel(int id)
    {
        var hotel = await _dataService.GetEntity(id);

        if (hotel is null)
        {
            return NotFound();
        }

        return Ok(hotel);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetHotels()
    {
        var hotels = await _dataService.GetEntities();

        return Ok(hotels);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteHotel(int id)
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
    public async Task<IActionResult> UpdateHotel(Hotel hotel)
    {
        await _dataService.UpdateEntity(hotel);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostHotel(Hotel hotel)
    {
        var added = await _dataService.PostEntity(hotel);

        return CreatedAtAction(nameof(GetHotel), new { id = added.Id }, added);
    }
}
