using HotelManagement.Models;
using HotelManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomStatusController : ControllerBase
{
    private readonly IRepository<RoomStatus> _repository;

    public RoomStatusController(IRepository<RoomStatus> repository)
    {
        _repository = repository;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetRoomStatus(int id)
    {
        var status = await _repository.GetEntity(status => status.Id == id);

        if (status is null)
        {
            return NotFound();
        }

        return Ok(status);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetRoomStatuses()
    {
        var statuses = await _repository.GetEntities(status => true);

        return Ok(statuses);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteRoomStatus(int id)
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
    public async Task<IActionResult> UpdateRoomStatus(RoomStatus status)
    {
        await _repository.Update(status);
        await _repository.SaveChanges();

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostRoomStatus(RoomStatus status)
    {
        var added = await _repository.Add(status);
        await _repository.SaveChanges();

        return CreatedAtAction(nameof(GetRoomStatus), new { id = added.Id }, added);
    }
}
