using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Models.Dtos;
using Web.Models.ViewModels;
using Web.Services;

namespace HotelManagement.Web.Controllers;

public class RoomAmenityController : Controller
{
    private readonly IGenericApiService<RoomAmenity> _roomAmenityService;
    private readonly IGenericApiService<Room> _roomService;

    public RoomAmenityController(
        IGenericApiService<RoomAmenity> roomAmenityService,
        IGenericApiService<Room> roomService
        )
    {
        _roomAmenityService = roomAmenityService;
        _roomService = roomService;
    }

    public async Task<IActionResult> Index()
    {
        var amenities = await _roomAmenityService.FetchEntities();

        var roomAmenityViewModels = new List<RoomAmenityViewModel>();

        foreach (var amenity in amenities)
        {
            var room = await _roomService.FetchEntity(amenity.RoomId);

            var amenityVM = new RoomAmenityViewModel
            {
                AmenityId = amenity.Id,
                Name = amenity.Name,
                RoomType = room.RoomType,
                RoomNumber = room.RoomNumber
            };

            roomAmenityViewModels.Add(amenityVM);
        }

        return View(roomAmenityViewModels);
    }

    public async Task<IActionResult> AddRoomAmenity()
    {
        var rooms = await _roomService.FetchEntities();

        var roomAmenityViewModel = new RoomAmenityViewModel
        {
            Rooms = new SelectList(rooms, "Id", "RoomType")
        };

        return View(roomAmenityViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddRoomAmenity(RoomAmenityViewModel roomAmenityViewModel)
    {
        var roomAmenity = new RoomAmenity
        {
            RoomId = roomAmenityViewModel.RoomId,
            Name = roomAmenityViewModel.Name
        };

        var addedAmenity = await _roomAmenityService.AddEntity(roomAmenity);

        if (addedAmenity is null)
        {
            return View();
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ManageRoomAmenity(int amenityId)
    {
        var roomAmenity = await _roomAmenityService.FetchEntity(amenityId);

        if (roomAmenity is null)
        {
            return NotFound($"Room Amenity with id = {amenityId} could not be found");
        }

        var rooms = await _roomService.FetchEntities();

        var roomAmenityViewModel = new RoomAmenityViewModel
        {
            Rooms = new SelectList(rooms, "Id", "RoomType"),
            Name = roomAmenity.Name,
            RoomId = roomAmenity.RoomId
        };

        return View(roomAmenityViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> ManageRoomAmenity(RoomAmenityViewModel roomAmenityViewModel, int amenityId)
    {
        var roomAmenity = await _roomAmenityService.FetchEntity(amenityId);

        if (roomAmenity is null)
        {
            return View();
        }

        roomAmenity.RoomId = roomAmenityViewModel.RoomId;
        roomAmenity.Name = roomAmenityViewModel.Name;

        var success = await _roomAmenityService.UpdateEntity(roomAmenity);

        if (!success)
        {
            return View();
        }

        return RedirectToAction(nameof(Index));
    }
}
