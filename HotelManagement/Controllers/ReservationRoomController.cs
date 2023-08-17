using HotelManagement.Models;
using HotelManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationRoomController : ControllerBase
{
    private readonly IRepository<ReservationRoom> _repository;

    public ReservationRoomController(IRepository<ReservationRoom> repository)
    {
        _repository = repository;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetReservationRoom(int id)
    {
        var reserveRoom = await _repository.GetEntity(reserveRoom => reserveRoom.Id == id);

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
        var reservationRooms = await _repository.GetEntities(reserveRoom => true);

        return Ok(reservationRooms);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteReservationRoom(int id)
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
    public async Task<IActionResult> UpdateReservationRoom(ReservationRoom reservationRoom)
    {
        await _repository.Update(reservationRoom);
        await _repository.SaveChanges();

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostReservationRoom(ReservationRoom reservationRoom)
    {
        var added = await _repository.Add(reservationRoom);
        await _repository.SaveChanges();

        return CreatedAtAction(nameof(GetReservationRoom), new { id = added.Id }, added);
    }
}
