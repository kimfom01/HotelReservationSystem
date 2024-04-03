using AutoMapper;
using HotelBackend.Old.Remove.DataAccess.Models;
using HotelBackend.Old.Remove.Api.Dtos;
using HotelBackend.Old.Remove.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Old.Remove.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PricingController : ControllerBase
{
    private readonly IPricingService _pricingService;
    private readonly IMapper _mapper;

    public PricingController(
        IPricingService pricingService,
        IMapper mapper)
    {
        _pricingService = pricingService;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetPricing(int id)
    {
        var pricing = await _pricingService.GetPricing(id);

        if (pricing is null)
        {
            return NotFound();
        }

        var pricingDto = _mapper.Map<PricingDto>(pricing);

        return Ok(pricingDto);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetPricings()
    {
        var pricings = await _pricingService.GetPricings();

        var pricingsDtos = _mapper.Map<IEnumerable<PricingDto>>(pricings);

        return Ok(pricingsDtos);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeletePricing(int id)
    {
        int deletedCount = await _pricingService.DeletePricing(id);

        if (deletedCount <= 0)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdatePricing(PricingDto pricingDto)
    {
        var pricing = _mapper.Map<Pricing>(pricingDto);

        await _pricingService.UpdatePricing(pricing);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostPricing(PricingDto pricingDto)
    {
        var pricing = _mapper.Map<Pricing>(pricingDto);

        var added = await _pricingService.PostPricing(pricing);

        var pricingResult = _mapper.Map<PricingDto>(added);

        return CreatedAtAction(nameof(GetPricing), new { id = pricingResult.Id }, pricingResult);
    }
}
