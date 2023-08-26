using HotelManagement.Web.Models.Dtos;
using HotelManagement.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Web.Controllers;

[Authorize(Roles = "SystemAdmin, HotelAdmin")]
public class PricingController : Controller
{
    private readonly IGenericApiService<Pricing> _pricingService;

    public PricingController(IGenericApiService<Pricing> pricingService)
    {
        _pricingService = pricingService;
    }

    public async Task<IActionResult> Index()
    {
        var pricings = await _pricingService.FetchEntities();

        return View(pricings);
    }

    public IActionResult AddPricing()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddPricing(Pricing pricing)
    {
        var addedPricing = await _pricingService.AddEntity(pricing);

        if (addedPricing is null)
        {
            return View();
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ManagePricing(int pricingId)
    {
        var price = await _pricingService.FetchEntity(pricingId);

        if (price is null)
        {
            return NotFound($"Pricing with id = {pricingId} cannot be found");
        }

        return View(price);
    }

    [HttpPost]
    public async Task<IActionResult> ManagePricing(Pricing newPricingInfo, int pricingId)
    {
        var pricing = await _pricingService.FetchEntity(pricingId);

        if (pricing is null)
        {
            return View();
        }

        pricing.RoomId = newPricingInfo.RoomId;
        pricing.Date = newPricingInfo.Date;
        pricing.NumberOfGuests = newPricingInfo.NumberOfGuests;
        pricing.Price = newPricingInfo.Price;

        var success = await _pricingService.UpdateEntity(pricing);

        if (!success)
        {
            return View();
        }

        return RedirectToAction(nameof(Index));
    }
}
