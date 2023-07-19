using HotelManagement.Models;
using HotelManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelController : ControllerBase
{
    private readonly Repository<Hotel> _repository;

    public HotelController(Repository<Hotel> repository)
    {
        _repository = repository;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetHotel(int id)
    {
        var hotel = await _repository.GetEntity(hotel => hotel.Id == id);

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
        var hotels = await _repository.GetEntities(hotel => true);

        return Ok(hotels);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteHotel(int id)
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
    public async Task<IActionResult> UpdateHotel(Hotel hotel)
    {
        await _repository.Update(hotel);
        await _repository.SaveChanges();

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostHotel(Hotel hotel)
    {
        var added = await _repository.Add(hotel);
        await _repository.SaveChanges();

        return CreatedAtAction(nameof(GetHotel), new { id = added.Id }, added);
    }
}
