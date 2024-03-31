using AutoMapper;
using HotelBackend.ReservationService.Dtos;
using HotelBackend.ReservationService.Models;
using HotelBackend.ReservationService.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.ReservationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IReservationService _reservationService;

    public ReservationController(
        IMapper mapper,
        IReservationService reservationService)
    {
        _mapper = mapper;
        _reservationService = reservationService;
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetReservation(Guid id)
    {
        var reservation = await _reservationService.GetReservation(id);

        if (reservation is null)
        {
            return NotFound();
        }

        var reservationDto = _mapper.Map<ReservationDto>(reservation);

        return Ok(reservationDto);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetReservations()
    {
        var reservations = await _reservationService.GetReservations();

        var reservationsDtos = _mapper.Map<IEnumerable<ReservationDto>>(reservations);

        return Ok(reservationsDtos);
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteReservation(Guid id)
    {
        int deletedCount = await _reservationService.DeleteReservation(id);

        if (deletedCount <= 0)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateReservation(ReservationDto reservationDto)
    {
        var reservation = _mapper.Map<Reservation>(reservationDto);

        await _reservationService.UpdateReservation(reservation);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostReservation(ReservationDto reservationDto)
    {
        var added = await _reservationService.MakeReservation(reservationDto);

        if (added is null)
        {
            return Conflict("Unable to make reservation, no available rooms");
        }

        return CreatedAtAction(nameof(GetReservation), new { id = added.Id }, added);
    }
}