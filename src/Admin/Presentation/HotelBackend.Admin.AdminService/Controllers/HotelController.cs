using System.Net;
using FluentValidation;
using HotelBackend.Admin.Application.Dtos.Hotels;
using HotelBackend.Admin.Application.Exceptions;
using HotelBackend.Admin.Application.Features.Hotels.Requests.Commands;
using HotelBackend.Admin.Application.Features.Hotels.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Admin.AdminService.Controllers;

[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class HotelController : ControllerBase
{
    private readonly IMediator _mediator;

    public HotelController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<GetHotelDto>>> GetHotels(CancellationToken cancellationToken)
    {
        var adminId = new Guid(User.Claims.FirstOrDefault(cl => cl.Type == "Id")?.Value ?? string.Empty);

        var hotelsDto = await _mediator.Send(new GetHotelListRequest
        {
            AdminId = adminId
        }, cancellationToken);

        return Ok(hotelsDto);
    }

    [HttpGet("{hotelId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<GetHotelDto>> GetHotel(Guid hotelId, CancellationToken cancellationToken)
    {
        try
        {
            var adminId = new Guid(User.Claims.FirstOrDefault(cl => cl.Type == "Id")?.Value ?? string.Empty);

            var hotel = await _mediator.Send(new GetHotelRequest
            {
                HotelId = hotelId,
                AdminId = adminId
            }, cancellationToken);

            return Ok(hotel);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<GetHotelDto>> CreateHotel(CreateHotelDto hotelDto,
        CancellationToken cancellationToken)
    {
        try
        {
            var adminId = new Guid(User.Claims.FirstOrDefault(cl => cl.Type == "Id")?.Value ?? string.Empty);

            var created = await _mediator.Send(new CreateHotelRequest
            {
                HotelDto = hotelDto,
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