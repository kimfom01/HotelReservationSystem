using System.Net;
using FluentValidation;
using HotelBackend.Admin.Application.Dtos.Rooms;
using HotelBackend.Admin.Application.Exceptions;
using HotelBackend.Admin.Application.Features.Rooms.Requests.Commands;
using HotelBackend.Admin.Application.Features.Rooms.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CreateRoomRequest = HotelBackend.Admin.Application.Dtos.Rooms.CreateRoomRequest;

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
    public async Task<ActionResult<GetRoomResponse>> GetAvailableRooms(Guid hotelId, CancellationToken cancellationToken)
    {
        var rooms = await _mediator.Send(new GetAvailableRoomsQuery
        {
            HotelId = hotelId
        }, cancellationToken);

        return Ok(rooms);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<GetRoomResponse>> CreateRoom(CreateRoomRequest roomRequest, CancellationToken cancellationToken)
    {
        try
        {
            var added = await _mediator.Send(new CreateRoomCommand
            {
                RoomRequest = roomRequest
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
    public async Task<ActionResult<GetRoomResponse>> GetRoomById(Guid hotelId, Guid roomId,
        CancellationToken cancellationToken)
    {
        try
        {
            var rooms = await _mediator.Send(new GetRoomByIdQuery
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
    public async Task<ActionResult<ReserveRoomResponse>> ReserveRoom(ReserveRoomRequest roomRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _mediator.Send(new ReserveRoomCommand
            {
                RoomRequest = roomRequest
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