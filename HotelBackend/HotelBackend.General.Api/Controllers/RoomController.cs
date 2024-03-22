using AutoMapper;
using DataAccess.Models;
using HotelBackend.General.Api.Dtos;
using HotelBackend.General.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.General.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IRoomService _roomService;
    private readonly IMapper _mapper;

    public RoomController(
        IRoomService roomService,
        IMapper mapper)
    {
        _roomService = roomService;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetRoom(int id)
    {
        var room = await _roomService.GetRoom(id);

        if (room is null)
        {
            return NotFound();
        }

        var roomDto = _mapper.Map<RoomDto>(room);

        return Ok(roomDto);
    }

    [HttpGet("hotel/{hotelId:int}/{capacity:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetRoomByHotelId(int hotelId, int capacity)
    {
        var room = await _roomService.GetRoom(room => room.HotelId == hotelId
                                                && room.Capacity == capacity);

        if (room is null)
        {
            return NotFound();
        }

        var roomDto = _mapper.Map<RoomDto>(room);

        return Ok(room);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetRooms()
    {
        var rooms = await _roomService.GetRooms();

        var roomsDtos = _mapper.Map<IEnumerable<RoomDto>>(rooms);

        return Ok(roomsDtos);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        int deletedCount = await _roomService.DeleteRoom(id);

        if (deletedCount <= 0)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateRoom(RoomDto roomDto)
    {
        var room = _mapper.Map<Room>(roomDto);

        await _roomService.UpdateRoom(room);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostRoom(RoomDto roomDto)
    {
        var room = _mapper.Map<Room>(roomDto);

        var added = await _roomService.PostRoom(room);

        var roomResult = _mapper.Map<RoomDto>(added);

        return CreatedAtAction(nameof(GetRoom), new { id = roomResult.Id }, roomResult);
    }
}
