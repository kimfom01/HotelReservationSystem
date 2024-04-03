using AutoMapper;
using HotelBackend.Old.Remove.DataAccess.Models;
using HotelBackend.Old.Remove.Api.Dtos;
using HotelBackend.Old.Remove.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Old.Remove.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelController : ControllerBase
{
    private readonly IHotelService _hotelService;
    private readonly IMapper _mapper;

    public HotelController(
        IHotelService hotelService,
        IMapper mapper)
    {
        _hotelService = hotelService;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetHotel(int id)
    {
        var hotel = await _hotelService.GetHotel(id);

        if (hotel is null)
        {
            return NotFound();
        }

        var hotelDto = _mapper.Map<HotelDto>(hotel);

        return Ok(hotelDto);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetHotels()
    {
        var hotels = await _hotelService.GetHotels();

        var hotelsDtos = _mapper.Map<IEnumerable<HotelDto>>(hotels);

        return Ok(hotelsDtos);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        int deletedCount = await _hotelService.DeleteHotel(id);

        if (deletedCount <= 0)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateHotel(HotelDto hotelDto)
    {
        var hotel = _mapper.Map<Hotel>(hotelDto);

        await _hotelService.UpdateHotel(hotel);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostHotel(HotelDto hotelDto)
    {
        var hotel = _mapper.Map<Hotel>(hotelDto);

        var added = await _hotelService.PostHotel(hotel);

        var hotelResult = _mapper.Map<HotelDto>(added);

        return CreatedAtAction(nameof(GetHotel), new { id = hotelResult.Id }, hotelResult);
    }
}
