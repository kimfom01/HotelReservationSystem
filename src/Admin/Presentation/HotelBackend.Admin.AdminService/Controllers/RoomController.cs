using System.Net;
using HotelBackend.Admin.Application.Dtos;
using HotelBackend.Admin.Application.Features.Rooms.Requests.Queries;
using MediatR;
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
    public async Task<ActionResult<RoomDto>> GetAvailableRooms(Guid hotelId)
    {
        var rooms = await _mediator.Send(new GetAvailableRoomsInHotelRequest { HotelId = hotelId });
        
        return Ok(rooms);
    }
}