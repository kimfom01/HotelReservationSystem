using HotelManagement.Models;
using HotelManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IRepository<Room> _repository;

    public RoomController(IRepository<Room> repository)
    {
        _repository = repository;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetRoom(int id)
    {
        var room = await _repository.GetEntity(room => room.Id == id);

        if (room is null)
        {
            return NotFound();
        }

        return Ok(room);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetRooms()
    {
        var rooms = await _repository.GetEntities(room => true);

        return Ok(rooms);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteRoom(int id)
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
    public async Task<IActionResult> UpdateRoom(Room room)
    {
        await _repository.Update(room);
        await _repository.SaveChanges();

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostRoom(Room room)
    {
        var added = await _repository.Add(room);
        await _repository.SaveChanges();

        return CreatedAtAction(nameof(GetRoom), new { id = added.Id }, added);
    }
}
