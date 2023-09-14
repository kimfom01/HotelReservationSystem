using Api.Dtos;
using Api.Services;
using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IDataServiceGeneric<Reservation> _dataService;
    private readonly IMapper _mapper;

    public ReservationController(IDataServiceGeneric<Reservation> dataService,
        IMapper mapper)
    {
        _dataService = dataService;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetReservation(int id)
    {
        var reservation = await _dataService.GetEntity(id);

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
        var reservations = await _dataService.GetEntities();

        var reservationsDtos = _mapper.Map<IEnumerable<ReservationDto>>(reservations);

        return Ok(reservationsDtos);
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
    public async Task<IActionResult> UpdateReservation(ReservationDto reservationDto)
    {
        var reservation = _mapper.Map<Reservation>(reservationDto);

        await _dataService.UpdateEntity(reservation);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostReservation(ReservationDto reservationDto)
    {
        var reservation = _mapper.Map<Reservation>(reservationDto);

        var added = await _dataService.PostEntity(reservation);

        var reservationResult = _mapper.Map<ReservationDto>(added);

        return CreatedAtAction(nameof(GetReservation), new { id = reservationResult.Id }, reservationResult);
    }
}
