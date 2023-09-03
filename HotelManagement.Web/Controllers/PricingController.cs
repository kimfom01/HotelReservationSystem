using HotelManagement.Web.Models.Dtos;
using HotelManagement.Web.Models.ViewModels;
using HotelManagement.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagement.Web.Controllers;

[Authorize(Roles = "SystemAdmin, HotelAdmin")]
public class PricingController : Controller
{
    private readonly IGenericApiService<Pricing> _pricingService;
    private readonly IGenericApiService<Room> _roomService;

    public PricingController(
        IGenericApiService<Pricing> pricingService,
        IGenericApiService<Room> roomService)
    {
        _pricingService = pricingService;
        _roomService = roomService;
    }

    public async Task<IActionResult> Index()
    {
        var pricings = await _pricingService.FetchEntities();

        return View(pricings);
    }

    public async Task<IActionResult> AddPricing()
    {
        var rooms = await _roomService.FetchEntities();

        var pricingViewModel = new PricingViewModel
        {
            Rooms = new SelectList(rooms, "Id", "RoomType")
        };

        return View(pricingViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddPricing(PricingViewModel pricingViewModel)
    {
        var pricing = new Pricing
        {
            RoomId = pricingViewModel.RoomId,
            Date = pricingViewModel.Date,
            NumberOfGuests = pricingViewModel.NumberOfGuests,
            Price = pricingViewModel.Price
        };

        var addedPricing = await _pricingService.AddEntity(pricing);

        if (addedPricing is null)
        {
            return View();
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ManagePricing(int pricingId)
    {
        var pricing = await _pricingService.FetchEntity(pricingId);

        if (pricing is null)
        {
            return NotFound($"Pricing with id = {pricingId} cannot be found");
        }

        var rooms = await _roomService.FetchEntities();

        var pricingViewModel = new PricingViewModel
        {
            Rooms = new SelectList(rooms, "Id", "RoomType"),
            Date = pricing.Date,
            RoomId = pricing.RoomId,
            NumberOfGuests = pricing.NumberOfGuests,
            Price = pricing.Price
        };

        return View(pricingViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> ManagePricing(PricingViewModel pricingViewModel, int pricingId)
    {
        var pricing = await _pricingService.FetchEntity(pricingId);

        if (pricing is null)
        {
            return View();
        }

        pricing.RoomId = pricingViewModel.RoomId;
        pricing.Date = pricingViewModel.Date;
        pricing.NumberOfGuests = pricingViewModel.NumberOfGuests;
        pricing.Price = pricingViewModel.Price;

        var success = await _pricingService.UpdateEntity(pricing);

        if (!success)
        {
            return View();
        }

        return RedirectToAction(nameof(Index));
    }
}
