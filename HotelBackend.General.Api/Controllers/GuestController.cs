using AutoMapper;
using DataAccess.Models;
using HotelBackend.General.Api.Dtos;
using HotelBackend.General.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.General.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuestController : ControllerBase
{
    private readonly IGuestService _guestService;
    private readonly IMapper _mapper;

    public GuestController(
        IGuestService guestService,
        IMapper mapper)
    {
        _guestService = guestService;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetGuest(int id)
    {
        var guest = await _guestService.GetGuest(id);

        if (guest is null)
        {
            return NotFound();
        }

        var guestDto = _mapper.Map<GuestDto>(guest);

        return Ok(guestDto);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetGuests()
    {
        var guests = await _guestService.GetGuests();

        if (guests is null)
        {
            return NotFound();
        }

        var guestsDtos = _mapper.Map<IEnumerable<GuestDto>>(guests);

        return Ok(guestsDtos);
    }

    [HttpGet("{emailAddress}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetGuestByEmailAddress(string emailAddress)
    {
        var guest = await _guestService.GetGuests(guest => guest.Email == emailAddress);

        if (guest is null)
        {
            return NotFound();
        }

        var guestDto = _mapper.Map<GuestDto>(guest);

        return Ok(guestDto);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteGuest(int id)
    {
        int deletedCount = await _guestService.DeleteGuest(id);

        if (deletedCount <= 0)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateGuest(GuestDto guestDto)
    {
        var guest = _mapper.Map<Guest>(guestDto);

        await _guestService.UpdateGuest(guest);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostGuest(GuestDto guestDto)
    {
        var guest = _mapper.Map<Guest>(guestDto);

        var added = await _guestService.PostGuest(guest);

        var guestDtoResult = _mapper.Map<GuestDto>(added);

        return CreatedAtAction(nameof(GetGuest), new { id = guestDtoResult.Id }, guestDtoResult);
    }
}
