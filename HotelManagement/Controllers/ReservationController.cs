using DataAccess.Models;
using HotelManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IDataServiceGeneric<Reservation> _dataService;

    public ReservationController(IDataServiceGeneric<Reservation> dataService)
    {
        _dataService = dataService;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetReservation(int id)
    {
        var reserve = await _dataService.GetEntity(id);

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
        var reservations = await _dataService.GetEntities();

        return Ok(reservations);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteReservation(int id)
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
    public async Task<IActionResult> UpdateReservation(Reservation reservation)
    {
        await _dataService.UpdateEntity(reservation);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostReservation(Reservation reservation)
    {
        var added = await _dataService.PostEntity(reservation);

        return CreatedAtAction(nameof(GetReservation), new { id = added.Id }, added);
    }
}
