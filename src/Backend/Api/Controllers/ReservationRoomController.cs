using Api.Dtos;
using Api.Services;
using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationRoomController : ControllerBase
{
    private readonly IDataServiceGeneric<ReservationRoom> _dataService;
    private readonly IMapper _mapper;

    public ReservationRoomController(IDataServiceGeneric<ReservationRoom> dataService,
        IMapper mapper)
    {
        _dataService = dataService;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetReservationRoom(int id)
    {
        var reservationRoom = await _dataService.GetEntity(id);

        if (reservationRoom is null)
        {
            return NotFound();
        }

        var reservationRoomDto = _mapper.Map<ReservationRoomDto>(reservationRoom);

        return Ok(reservationRoomDto);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetReservationRooms()
    {
        var reservationRooms = await _dataService.GetEntities();

        var reservationRoomsDtos = _mapper.Map<IEnumerable<ReservationRoomDto>>(reservationRooms);

        return Ok(reservationRoomsDtos);
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
    public async Task<IActionResult> UpdateReservationRoom(ReservationRoomDto reservationRoomDto)
    {
        var reservationRoom = _mapper.Map<ReservationRoom>(reservationRoomDto);

        await _dataService.UpdateEntity(reservationRoom);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostReservationRoom(ReservationRoomDto reservationRoomDto)
    {
        var reservationRoom = _mapper.Map<ReservationRoom>(reservationRoomDto);

        var added = await _dataService.PostEntity(reservationRoom);

        var reservationRoomDtoResult = _mapper.Map<ReservationRoomDto>(added);

        return CreatedAtAction(nameof(GetReservationRoom), new { id = reservationRoomDtoResult.Id }, reservationRoomDtoResult);
    }
}
