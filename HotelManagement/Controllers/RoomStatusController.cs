using DataAccess.Models;
using HotelManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomStatusController : ControllerBase
{
    private readonly IDataServiceGeneric<RoomStatus> _dataService;

    public RoomStatusController(IDataServiceGeneric<RoomStatus> dataService)
    {
        _dataService = dataService;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetRoomStatus(int id)
    {
        var status = await _dataService.GetEntity(status => status.Id == id);

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
        var statuses = await _dataService.GetEntities();

        return Ok(statuses);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteRoomStatus(int id)
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
    public async Task<IActionResult> UpdateRoomStatus(RoomStatus status)
    {
        await _dataService.UpdateEntity(status);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostRoomStatus(RoomStatus status)
    {
        var added = await _dataService.PostEntity(status);

        return CreatedAtAction(nameof(GetRoomStatus), new { id = added.Id }, added);
    }
}
