using System.Net;
using FluentValidation;
using Admin.Application.Dtos.Admin.Hotels;
using Admin.Application.Features.Admin.Hotels.Commands;
using Admin.Application.Features.Admin.Hotels.Queries;
using Asp.Versioning;
using Hrs.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Presentation.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("/api/v{v:apiVersion}/[controller]")]
public class HotelController : ControllerBase
{
    private readonly IMediator _mediator;

    public HotelController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [MapToApiVersion(1)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<GetHotelResponse>>> GetHotels(CancellationToken cancellationToken)
    {
        var hotelsDto = await _mediator.Send(new GetHotelListQuery(), cancellationToken);

        return Ok(hotelsDto);
    }

    [HttpGet("{hotelId:guid}")]
    [MapToApiVersion(1)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<GetHotelResponse>> GetHotel(Guid hotelId, CancellationToken cancellationToken)
    {
        try
        {
            var hotel = await _mediator.Send(new GetHotelQuery
            {
                HotelId = hotelId
            }, cancellationToken);

            return Ok(hotel);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    [MapToApiVersion(1)]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<GetHotelResponse>> CreateHotel(CreateHotelRequest hotelRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            var adminId = new Guid(User.Claims.FirstOrDefault(cl => cl.Type == "Id")?.Value ?? string.Empty);

            var created = await _mediator.Send(new CreateHotelCommand
            {
                HotelRequest = hotelRequest,
                AdminId = adminId
            }, cancellationToken);

            return CreatedAtAction(nameof(GetHotel), new { hotelId = created.Id }, created);
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
}