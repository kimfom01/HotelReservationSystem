using System.Net;
using HotelBackend.Reservations.Application.Dtos;
using HotelBackend.Reservations.Application.Features.Hotels.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Reservations.ReservationService.Controllers;

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
    public async Task<ActionResult<HotelDto>> GetHotels()
    {
        var hotelsDto = await _mediator.Send(new GetHotelListRequest());
        
        return Ok(hotelsDto);
    }
}