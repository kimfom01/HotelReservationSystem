using System.Net;
using FluentValidation;
using Hrs.Application.Dtos.Admin.RoomTypes;
using Hrs.Application.Exceptions;
using Hrs.Application.Features.Admin.RoomTypes.Requests.Command;
using Hrs.Application.Features.Admin.RoomTypes.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hrs.Presentation.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class RoomTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoomTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("s")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<GetRoomTypeResponse>> GetRoomType(Guid roomTypeId, Guid hotelId,
        CancellationToken cancellationToken)
    {
        try
        {
            var roomType = await _mediator.Send(new GetRoomTypeQuery
            {
                RoomTypeId = roomTypeId,
                HotelId = hotelId
            }, cancellationToken);

            return Ok(roomType);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<GetRoomTypeResponse>> CreateRoomType(CreateRoomTypeRequest roomTypeRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            var roomType = await _mediator.Send(
                new CreateRoomTypeCommand
                {
                    RoomTypeRequest = roomTypeRequest
                }, cancellationToken);

            return CreatedAtAction(nameof(GetRoomType), new
                {
                    roomTypeId = roomType.Id,
                    hotelId = roomType.HotelId
                },
                roomType);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<GetRoomTypeResponse>>> GetRoomTypes(Guid hotelId,
        CancellationToken cancellationToken)
    {
        var roomTypes = await _mediator.Send(new GetRoomTypeListQuery
        {
            HotelId = hotelId
        }, cancellationToken);

        return Ok(roomTypes);
    }
}