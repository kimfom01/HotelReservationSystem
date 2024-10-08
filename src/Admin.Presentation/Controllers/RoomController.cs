using System.Net;
using FluentValidation;
using Admin.Application.Dtos.Admin.Rooms;
using Admin.Application.Features.Admin.Rooms.Commands;
using Admin.Application.Features.Admin.Rooms.Queries;
using Asp.Versioning;
using Hrs.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Presentation.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("/api/v{v:apiVersion}/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoomController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("available/{hotelId:Guid}")]
    [MapToApiVersion(1)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetRoomResponse>> GetAvailableRooms(Guid hotelId,
        CancellationToken cancellationToken)
    {
        var rooms = await _mediator.Send(new GetAvailableRoomsQuery
        {
            HotelId = hotelId
        }, cancellationToken);

        return Ok(rooms);
    }

    [Authorize]
    [HttpPost]
    [MapToApiVersion(1)]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<GetRoomResponse>> CreateRoom(CreateRoomRequest roomRequest,
        CancellationToken cancellationToken)
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

    [Authorize]
    [HttpPost("m")]
    [MapToApiVersion(1)]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<GetRoomResponse>> CreateManyRooms(CreateManyRoomsRequest roomsRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            var count = await _mediator.Send(new CreateManyRoomsCommand
            {
                RoomsRequest = roomsRequest
            }, cancellationToken);

            return Ok($"{count} rooms created");
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [MapToApiVersion(1)]
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
}