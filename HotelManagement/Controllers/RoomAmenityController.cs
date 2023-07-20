﻿using HotelManagement.Models;
using HotelManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RoomAmenityController : ControllerBase
{
    private readonly IRepository<RoomAmenity> _repository;

    public RoomAmenityController(IRepository<RoomAmenity> repository)
    {
        _repository = repository;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAmenity(int id)
    {
        var amenity = await _repository.GetEntity(amenity => amenity.Id == id);

        if (amenity is null)
        {
            return NotFound();
        }

        return Ok(amenity);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetAmenities()
    {
        var amenities = await _repository.GetEntities(amenity => true);

        return Ok(amenities);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteAmenity(int id)
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
    public async Task<IActionResult> UpdateAmenity(RoomAmenity amenity)
    {
        await _repository.Update(amenity);
        await _repository.SaveChanges();

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostHotel(RoomAmenity amenity)
    {
        var added = await _repository.Add(amenity);
        await _repository.SaveChanges();

        return CreatedAtAction(nameof(GetAmenity), new { id = added.Id }, added);
    }
}