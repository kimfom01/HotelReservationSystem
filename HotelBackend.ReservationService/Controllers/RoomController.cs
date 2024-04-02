using HotelBackend.ReservationService.Dtos;
using HotelBackend.ReservationService.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.ReservationService.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IRoomService _roomService;
    private readonly ILogger<RoomController> _logger;

    public RoomController(IRoomService roomService, ILogger<RoomController> logger)
    {
        _roomService = roomService;
        _logger = logger;
    }

    [HttpGet("available/{hotelId:Guid}")]
    public async Task<ActionResult<RoomDto>> GetAvailableRooms(Guid hotelId)
    {
        var rooms = await _roomService.GetAvailableRoomsPerRoomCapacity(hotelId);

        if (!rooms.Any())
        {
            _logger.LogError("There are no rooms found or registered in Hotel={hotelId}", hotelId);
            return NotFound($"There are no rooms found or registered in Hotel={hotelId}");
        }

        _logger.LogInformation("Getting available rooms from Hotel={hotelId}", hotelId);
        return Ok(rooms);
    }
}