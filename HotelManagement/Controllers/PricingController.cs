using DataAccess.Models;
using HotelManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PricingController : ControllerBase
{
    private readonly IDataServiceGeneric<Pricing> _dataService;

    public PricingController(IDataServiceGeneric<Pricing> dataService)
    {
        _dataService = dataService;
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

        return Ok(pricing);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetPricings()
    {
        var pricings = await _dataService.GetEntities();

        return Ok(pricings);
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
    public async Task<IActionResult> UpdatePricing(Pricing pricing)
    {
        await _dataService.UpdateEntity(pricing);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostPricing(Pricing pricing)
    {
        var added = await _dataService.PostEntity(pricing);

        return CreatedAtAction(nameof(GetPricing), new { id = added.Id }, added);
    }
}
