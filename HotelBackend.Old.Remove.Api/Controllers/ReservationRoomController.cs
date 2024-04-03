using AutoMapper;
using HotelBackend.Old.Remove.DataAccess.Models;
using HotelBackend.Old.Remove.Api.Dtos;
using HotelBackend.Old.Remove.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Old.Remove.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationRoomController : ControllerBase
{
    private readonly IReservationRoomService _reservationRoomService;
    private readonly IMapper _mapper;

    public ReservationRoomController(
        IMapper mapper,
        IReservationRoomService reservationRoomService)
    {
        _mapper = mapper;
        _reservationRoomService = reservationRoomService;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetReservationRoom(int id)
    {
        var reservationRoom = await _reservationRoomService.GetReservationRoom(id);

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
        var reservationRooms = await _reservationRoomService.GetReservationRooms();

        var reservationRoomsDtos = _mapper.Map<IEnumerable<ReservationRoomDto>>(reservationRooms);

        return Ok(reservationRoomsDtos);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteReservationRoom(int id)
    {
        int deletedCount = await _reservationRoomService.DeleteReservationRoom(id);

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

        await _reservationRoomService.UpdateReservationRoom(reservationRoom);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostReservationRoom(ReservationRoomDto reservationRoomDto)
    {
        var reservationRoom = _mapper.Map<ReservationRoom>(reservationRoomDto);

        var added = await _reservationRoomService.AddReservationRoom(reservationRoom);

        var reservationRoomDtoResult = _mapper.Map<ReservationRoomDto>(added);

        return CreatedAtAction(nameof(GetReservationRoom), new { id = reservationRoomDtoResult.Id }, reservationRoomDtoResult);
    }
}
