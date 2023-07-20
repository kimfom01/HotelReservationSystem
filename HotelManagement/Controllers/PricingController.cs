using HotelManagement.Models;
using HotelManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PricingController : ControllerBase
{
    private readonly IRepository<Pricing> _repository;

    public PricingController(IRepository<Pricing> repository)
    {
        _repository = repository;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetPricing(int id)
    {
        var pricing = await _repository.GetEntity(pricing => pricing.Id == id);

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
        var pricings = await _repository.GetEntities(pricing => true);

        return Ok(pricings);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeletePricing(int id)
    {
        await _repository.Delete(id);
        int deltedCount = await _repository.SaveChanges();

        if (deltedCount <= 0)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdatePricing(Pricing pricing)
    {
        await _repository.Update(pricing);
        await _repository.SaveChanges();

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostPricing(Pricing pricing)
    {
        var added = await _repository.Add(pricing);
        await _repository.SaveChanges();

        return CreatedAtAction(nameof(GetPricing), new { id = added.Id }, added);
    }
}
