using HotelManagement.Models;
using HotelManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IRepository<Reservation> _repository;

    public ReservationController(IRepository<Reservation> repository)
    {
        _repository = repository;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetReservation(int id)
    {
        var reserve = await _repository.GetEntity(reserve => reserve.Id == id);

        if (reserve is null)
        {
            return NotFound();
        }

        return Ok(reserve);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetReservations()
    {
        var reservations = await _repository.GetEntities(reserve => true);

        return Ok(reservations);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteReservation(int id)
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
    public async Task<IActionResult> UpdateReservation(Reservation reservation)
    {
        await _repository.Update(reservation);
        await _repository.SaveChanges();

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostReservation(Reservation reservation)
    {
        var added = await _repository.Add(reservation);
        await _repository.SaveChanges();

        return CreatedAtAction(nameof(GetReservation), new { id = added.Id }, added);
    }
}
