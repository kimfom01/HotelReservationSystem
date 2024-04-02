using HotelBackend.ReservationService.Dtos;
using HotelBackend.ReservationService.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.ReservationService.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class HotelController : ControllerBase
{
    private readonly IHotelService _hotelService;
    private readonly ILogger<HotelController> _logger;

    public HotelController(IHotelService hotelService, ILogger<HotelController> logger)
    {
        _hotelService = hotelService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<HotelDto>> GetHotels()
    {
        var hotelsDto = await _hotelService.GetHotels();

        if (hotelsDto is null)
        {
            _logger.LogError("There are no hotels found or registered!");
            return NotFound("There are no hotels found or registered!");
        }

        _logger.LogInformation("Getting list of hotels");
        return Ok(hotelsDto);
    }
}