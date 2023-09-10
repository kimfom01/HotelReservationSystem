using Api.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuestController : ControllerBase
{
    private readonly IDataServiceGeneric<Guest> _dataService;

    public GuestController(IDataServiceGeneric<Guest> dataService)
    {
        _dataService = dataService;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetGuest(int id)
    {
        var guest = await _dataService.GetEntity(id);

        if (guest is null)
        {
            return NotFound();
        }

        return Ok(guest);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetGuests()
    {
        var guests = await _dataService.GetEntities();

        if (guests is null)
        {
            return NotFound();
        }

        return Ok(guests);
    }

    [HttpGet("{emailAddress}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetGuestByEmailAddress(string emailAddress)
    {
        var guest = await _dataService.GetEntity(guest => guest.Email == emailAddress);

        if (guest is null)
        {
            return NotFound();
        }

        return Ok(guest);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteGuest(int id)
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
    public async Task<IActionResult> UpdateGuest(Guest guest)
    {
        await _dataService.UpdateEntity(guest);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostGuest(Guest guest)
    {
        var added = await _dataService.PostEntity(guest);

        return CreatedAtAction(nameof(GetGuest), new { id = added.Id }, added);
    }
}
