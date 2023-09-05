using DataAccess.Models;
using HotelManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationRoomController : ControllerBase
{
    private readonly IDataServiceGeneric<ReservationRoom> _dataService;

    public ReservationRoomController(IDataServiceGeneric<ReservationRoom> dataService)
    {
        _dataService = dataService;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetReservationRoom(int id)
    {
        var reserveRoom = await _dataService.GetEntity(id);

        if (reserveRoom is null)
        {
            return NotFound();
        }

        return Ok(reserveRoom);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetReservationRooms()
    {
        var reservationRooms = await _dataService.GetEntities();

        return Ok(reservationRooms);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteReservationRoom(int id)
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
    public async Task<IActionResult> UpdateReservationRoom(ReservationRoom reservationRoom)
    {
        await _dataService.UpdateEntity(reservationRoom);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostReservationRoom(ReservationRoom reservationRoom)
    {
        var added = await _dataService.PostEntity(reservationRoom);

        return CreatedAtAction(nameof(GetReservationRoom), new { id = added.Id }, added);
    }
}
