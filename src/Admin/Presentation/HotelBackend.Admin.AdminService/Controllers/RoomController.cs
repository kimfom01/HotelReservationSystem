using System.Net;
using FluentValidation;
using HotelBackend.Admin.Application.Dtos.Rooms;
using HotelBackend.Admin.Application.Exceptions;
using HotelBackend.Admin.Application.Features.Rooms.Requests.Commands;
using HotelBackend.Admin.Application.Features.Rooms.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Admin.AdminService.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoomController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("available/{hotelId:Guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetRoomDto>> GetAvailableRooms(Guid hotelId, CancellationToken cancellationToken)
    {
        var rooms = await _mediator.Send(new GetAvailableRoomsRequest
        {
            HotelId = hotelId
        }, cancellationToken);

        return Ok(rooms);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<GetRoomDto>> CreateRoom(CreateRoomDto roomDto, CancellationToken cancellationToken)
    {
        try
        {
            var added = await _mediator.Send(new CreateRoomRequest
            {
                RoomDto = roomDto
            }, cancellationToken);

            return CreatedAtAction(nameof(GetRoomById), new
            {
                hotelId = added.HotelId, roomId = added.Id
            }, added);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<GetRoomDto>> GetRoomById(Guid hotelId, Guid roomId,
        CancellationToken cancellationToken)
    {
        try
        {
            var rooms = await _mediator.Send(new GetRoomByIdRequest
            {
                HotelId = hotelId,
                RoomId = roomId
            }, cancellationToken);

            return Ok(rooms);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("hold")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ReserveRoomResponse>> ReserveRoom(ReserveRoomRequestDto roomRequestDto,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _mediator.Send(new ReserveRoomRequest
            {
                RoomRequestDto = roomRequestDto
            }, cancellationToken);

            return Ok(response);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}