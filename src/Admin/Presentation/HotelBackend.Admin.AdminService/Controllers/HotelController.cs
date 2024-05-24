using System.Net;
using HotelBackend.Admin.Application.Dtos;
using HotelBackend.Admin.Application.Features.Hotels.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Admin.AdminService.Controllers;

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