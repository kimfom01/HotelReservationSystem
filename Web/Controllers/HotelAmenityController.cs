using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Models.Dtos;
using Web.Models.ViewModels;
using Web.Services;

namespace HotelManagement.Web.Controllers;

public class HotelAmenityController : Controller
{
    private readonly IGenericApiService<HotelAmenity> _hotelAmenityService;
    private readonly IGenericApiService<Hotel> _hotelService;

    public HotelAmenityController(
        IGenericApiService<HotelAmenity> hotelAmenityService,
        IGenericApiService<Hotel> hotelService
        )
    {
        _hotelAmenityService = hotelAmenityService;
        _hotelService = hotelService;
    }

    public async Task<IActionResult> Index()
    {
        var amenities = await _hotelAmenityService.FetchEntities();
        var amenityViewModelList = new List<HotelAmenityViewModel>();

        foreach (var amenity in amenities)
        {
            var hotel = await _hotelService.FetchEntity(amenity.HotelId);
            amenityViewModelList.Add(new HotelAmenityViewModel
            {
                HotelName = hotel.Name,
                Amenity = amenity.Name,
                HotelAmenityId = amenity.Id
            });
        }

        return View(amenityViewModelList);
    }

    public async Task<IActionResult> AddHotelAmenity()
    {
        var hotels = await _hotelService.FetchEntities();

        var amenityViewModel = new HotelAmenityViewModel
        {
            Hotels = new SelectList(hotels, "Id", "Name")
        };

        return View(amenityViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddHotelAmenity(HotelAmenityViewModel hotelAmenityViewModel)
    {
        var hotelAmenity = new HotelAmenity
        {
            HotelId = hotelAmenityViewModel.HotelId,
            Name = hotelAmenityViewModel.Amenity
        };

        var addedHotelAmenity = await _hotelAmenityService.AddEntity(hotelAmenity);

        if (addedHotelAmenity is null)
        {
            return View();
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ManageHotelAmenity(int amenityId)
    {
        var amenity = await _hotelAmenityService.FetchEntity(amenityId);

        if (amenity is null)
        {
            return NotFound($"Hotel Amenity with id = {amenityId} cannot be found");
        }

        var hotels = await _hotelService.FetchEntities();

        var amenityViewModel = new HotelAmenityViewModel
        {
            Hotels = new SelectList(hotels, "Id", "Name"),
            Amenity = amenity.Name,
            HotelId = amenity.HotelId
        };

        return View(amenityViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> ManageHotelAmenity(HotelAmenityViewModel hotelAmenityViewModel, int amenityId)
    {
        var amenity = await _hotelAmenityService.FetchEntity(amenityId);

        if (amenity is null)
        {
            return View();
        }

        amenity.HotelId = hotelAmenityViewModel.HotelId;
        amenity.Name = hotelAmenityViewModel.Amenity;

        var success = await _hotelAmenityService.UpdateEntity(amenity);

        if (!success)
        {
            return View();
        }

        return RedirectToAction(nameof(Index));
    }
}
