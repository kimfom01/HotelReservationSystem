using Api.Dtos;
using Api.Services;
using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PricingController : ControllerBase
{
    private readonly IDataServiceGeneric<Pricing> _dataService;
    private readonly IMapper _mapper;

    public PricingController(IDataServiceGeneric<Pricing> dataService,
        IMapper mapper)
    {
        _dataService = dataService;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetPricing(int id)
    {
        var pricing = await _dataService.GetEntity(id);

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
        var pricings = await _dataService.GetEntities();

        var pricingsDtos = _mapper.Map<IEnumerable<PricingDto>>(pricings);

        return Ok(pricingsDtos);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeletePricing(int id)
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
    public async Task<IActionResult> UpdatePricing(PricingDto pricingDto)
    {
        var pricing = _mapper.Map<Pricing>(pricingDto);

        await _dataService.UpdateEntity(pricing);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostPricing(PricingDto pricingDto)
    {
        var pricing = _mapper.Map<Pricing>(pricingDto);

        var added = await _dataService.PostEntity(pricing);

        var pricingResult = _mapper.Map<PricingDto>(added);

        return CreatedAtAction(nameof(GetPricing), new { id = pricingResult.Id }, pricingResult);
    }
}
