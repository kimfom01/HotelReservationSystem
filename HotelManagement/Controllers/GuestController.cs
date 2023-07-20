using HotelManagement.Models;
using HotelManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuestController : ControllerBase
{
    private readonly IRepository<Guest> _repository;

    public GuestController(IRepository<Guest> repository)
    {
        _repository = repository;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetGuest(int id)
    {
        var guest = await _repository.GetEntity(guest => guest.Id == id);

        if (guest is null)
        {
            return NotFound();
        }

        return Ok(guest);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetGuests()
    {
        var guests = await _repository.GetEntities(guest => true);

        return Ok(guests);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteGuest(int id)
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
    public async Task<IActionResult> UpdateGuest(Guest guest)
    {
        await _repository.Update(guest);
        await _repository.SaveChanges();

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostGuest(Guest guest)
    {
        var added = await _repository.Add(guest);
        await _repository.SaveChanges();

        return CreatedAtAction(nameof(GetGuest), new { id = added.Id }, added);
    }
}
