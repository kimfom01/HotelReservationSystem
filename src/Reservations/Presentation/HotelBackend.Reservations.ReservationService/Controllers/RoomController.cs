using System.Net;
using HotelBackend.Reservations.Application.Dtos;
using HotelBackend.Reservations.Application.Features.Rooms.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Reservations.ReservationService.Controllers;

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