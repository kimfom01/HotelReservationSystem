using Api.Dtos;
using Api.Services;
using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelController : ControllerBase
{
    private readonly IDataServiceGeneric<Hotel> _dataService;
    private readonly IMapper _mapper;

    public HotelController(IDataServiceGeneric<Hotel> dataService,
        IMapper mapper)
    {
        _dataService = dataService;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetHotel(int id)
    {
        var hotel = await _dataService.GetEntity(id);

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
        var hotels = await _dataService.GetEntities();

        var hotelsDtos = _mapper.Map<IEnumerable<HotelDto>>(hotels);

        return Ok(hotelsDtos);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        int deletedCount = await _dataService.DeleteEntity(id);

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

        await _dataService.UpdateEntity(hotel);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostHotel(HotelDto hotelDto)
    {
        var hotel = _mapper.Map<Hotel>(hotelDto);

        var added = await _dataService.PostEntity(hotel);

        var hotelResult = _mapper.Map<HotelDto>(added);

        return CreatedAtAction(nameof(GetHotel), new { id = hotelResult.Id }, hotelResult);
    }
}
