using HotelBackend.Application.Dtos;
using HotelBackend.Application.Features.Hotels.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.ReservationService.Controllers;

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
    public async Task<ActionResult<HotelDto>> GetHotels()
    {
        var hotelsDto = await _mediator.Send(new GetHotelListRequest());
        
        return Ok(hotelsDto);
    }
}