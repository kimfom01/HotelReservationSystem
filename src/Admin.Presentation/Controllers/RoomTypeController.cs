using System.Net;
using FluentValidation;
using Admin.Application.Dtos.Admin.RoomTypes;
using Admin.Application.Features.Admin.RoomTypes.Command;
using Admin.Application.Features.Admin.RoomTypes.Queries;
using Asp.Versioning;
using Hrs.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Presentation.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("/api/v{v:apiVersion}/[controller]")]
public class RoomTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoomTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("s")]
    [MapToApiVersion(1)]
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
    [MapToApiVersion(1)]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<GetRoomTypeResponse>> CreateRoomType(CreateRoomTypeRequest roomTypeRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            var roomType = await _mediator.Send(new CreateRoomTypeCommand(roomTypeRequest), cancellationToken);

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
    [MapToApiVersion(1)]
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