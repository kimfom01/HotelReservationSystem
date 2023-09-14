using Api.Dtos;
using Api.Services;
using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomStatusController : ControllerBase
{
    private readonly IDataServiceGeneric<RoomStatus> _dataService;
    private readonly IMapper _mapper;

    public RoomStatusController(IDataServiceGeneric<RoomStatus> dataService,
        IMapper mapper)
    {
        _dataService = dataService;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetRoomStatus(int id)
    {
        var status = await _dataService.GetEntity(status => status.Id == id);

        if (status is null)
        {
            return NotFound();
        }

        var statusDto = _mapper.Map<RoomStatusDto>(status);

        return Ok(statusDto);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetRoomStatuses()
    {
        var statuses = await _dataService.GetEntities();

        var statusesDtos = _mapper.Map<IEnumerable<RoomStatusDto>>(statuses);

        return Ok(statusesDtos);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteRoomStatus(int id)
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
    public async Task<IActionResult> UpdateRoomStatus(RoomStatusDto statusDto)
    {
        var status = _mapper.Map<RoomStatus>(statusDto);

        await _dataService.UpdateEntity(status);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostRoomStatus(RoomStatusDto statusDto)
    {
        var status = _mapper.Map<RoomStatus>(statusDto);

        var added = await _dataService.PostEntity(status);

        var statusDtoResult = _mapper.Map<RoomStatusDto>(added);

        return CreatedAtAction(nameof(GetRoomStatus), new { id = statusDtoResult.Id }, statusDtoResult);
    }
}
