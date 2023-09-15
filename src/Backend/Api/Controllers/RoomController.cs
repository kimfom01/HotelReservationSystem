using Api.Dtos;
using Api.Services;
using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IDataServiceGeneric<Room> _dataService;
    private readonly IMapper _mapper;

    public RoomController(IDataServiceGeneric<Room> dataService,
        IMapper mapper)
    {
        _dataService = dataService;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetRoom(int id)
    {
        var room = await _dataService.GetEntity(id);

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
        var room = await _dataService.GetEntity(room => room.HotelId == hotelId
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
        var rooms = await _dataService.GetEntities();

        var roomsDtos = _mapper.Map<IEnumerable<RoomDto>>(rooms);

        return Ok(roomsDtos);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteRoom(int id)
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
    public async Task<IActionResult> UpdateRoom(RoomDto roomDto)
    {
        var room = _mapper.Map<Room>(roomDto);

        await _dataService.UpdateEntity(room);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostRoom(RoomDto roomDto)
    {
        var room = _mapper.Map<Room>(roomDto);

        var added = await _dataService.PostEntity(room);

        var roomResult = _mapper.Map<RoomDto>(added);

        return CreatedAtAction(nameof(GetRoom), new { id = roomResult.Id }, roomResult);
    }
}
