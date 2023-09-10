using Api.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IDataServiceGeneric<Room> _dataService;

    public RoomController(IDataServiceGeneric<Room> dataService)
    {
        _dataService = dataService;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetRoom(int id)
    {
        var room = await _dataService.GetEntity(id);

        if (room is null)
        {
            return NotFound();
        }

        return Ok(room);
    }

    [HttpGet("hotel/{hotelId:int}/{capacity:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetRoomByHotelId(int hotelId, int capacity)
    {
        var room = await _dataService.GetEntity(room => room.HotelId == hotelId
                                                && room.Capacity == capacity);

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
        var rooms = await _dataService.GetEntities();

        return Ok(rooms);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteRoom(int id)
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
    public async Task<IActionResult> UpdateRoom(Room room)
    {
        await _dataService.UpdateEntity(room);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostRoom(Room room)
    {
        var added = await _dataService.PostEntity(room);

        return CreatedAtAction(nameof(GetRoom), new { id = added.Id }, added);
    }
}
