using HotelManagement.Web.Models.Dtos;
using HotelManagement.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Web.Controllers;

[Authorize(Roles = "SystemAdmin, HotelAdmin")]
public class HotelController : Controller
{
    private readonly IGenericApiService<Hotel> _hotelService;

    public HotelController(IGenericApiService<Hotel> hotelService)
    {
        _hotelService = hotelService;
    }

    public async Task<IActionResult> Index()
    {
        var hotels = await _hotelService.FetchEntities();

        return View(hotels);
    }

    public IActionResult AddHotel()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddHotel(Hotel hotel)
    {
        var addedHotel = await _hotelService.AddEntity(hotel);

        if (addedHotel is null)
        {
            return View();
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ManageHotel(int hotelId)
    {
        var hotel = await _hotelService.FetchEntity(hotelId);

        if (hotel is null)
        {
            return NotFound($"Hotel with id = {hotelId} cannot be found");
        }

        return View(hotel);
    }

    [HttpPost]
    public async Task<IActionResult> ManageHotel(Hotel newHotelInfo, int id)
    {
        var hotel = await _hotelService.FetchEntity(id);

        if (hotel is null)
        {
            return View();
        }

        hotel.Name = newHotelInfo.Name;
        hotel.Location = newHotelInfo.Location;

        var success = await _hotelService.UpdateEntity(hotel);

        if (success)
        {
            return View();
        }

        return RedirectToAction(nameof(Index));
    }
}
