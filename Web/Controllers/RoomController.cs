using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Models.Dtos;
using Web.Models.ViewModels;
using Web.Services;

namespace HotelManagement.Web.Controllers;

public class RoomController : Controller
{
    private readonly IGenericApiService<Room> _roomService;
    private readonly IGenericApiService<Hotel> _hotelService;

    public RoomController(
        IGenericApiService<Room> roomService,
        IGenericApiService<Hotel> hotelService
        )
    {
        _roomService = roomService;
        _hotelService = hotelService;
    }

    public async Task<IActionResult> Index()
    {
        var rooms = await _roomService.FetchEntities();

        var roomViewModelList = new List<RoomViewModel>();

        foreach (var room in rooms)
        {
            var hotel = await _hotelService.FetchEntity(room.HotelId);

            roomViewModelList.Add(new RoomViewModel
            {
                HotelName = hotel.Name,
                RoomNumber = room.RoomNumber,
                Capacity = room.Capacity,
                RoomType = room.RoomType,
                RoomPrice = room.RoomPrice,
                Availabilty = room.Availabilty,
                RoomId = room.Id
            });
        }

        return View(roomViewModelList);
    }

    public async Task<IActionResult> AddRoom()
    {
        var hotels = await _hotelService.FetchEntities();

        var roomViewModel = new RoomViewModel
        {
            Hotels = new SelectList(hotels, "Id", "Name")
        };

        return View(roomViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddRoom(RoomViewModel roomViewModel)
    {
        var room = new Room
        {
            HotelId = roomViewModel.HotelId,
            RoomNumber = roomViewModel.RoomNumber,
            Capacity = roomViewModel.Capacity,
            RoomType = roomViewModel.RoomType,
            RoomPrice = roomViewModel.RoomPrice
        };

        var addedRoom = await _roomService.AddEntity(room);

        if (addedRoom is null)
        {
            return View();
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ManageRoom(int roomId)
    {
        var room = await _roomService.FetchEntity(roomId);

        if (room is null)
        {
            return NotFound($"Room with id {roomId} not found");
        }

        var hotels = await _hotelService.FetchEntities();

        var roomViewModel = new RoomViewModel
        {
            HotelId = room.HotelId,
            RoomNumber = room.RoomNumber,
            Capacity = room.Capacity,
            RoomType = room.RoomType,
            RoomPrice = room.RoomPrice,
            Availabilty = room.Availabilty,
            Hotels = new SelectList(hotels, "Id", "Name")
        };

        return View(roomViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> ManageRoom(RoomViewModel roomViewModel, int roomId)
    {
        var room = await _roomService.FetchEntity(roomId);

        if (room is null)
        {
            return View();
        }

        room.HotelId = roomViewModel.HotelId;
        room.RoomNumber = roomViewModel.RoomNumber;
        room.Capacity = roomViewModel.Capacity;
        room.RoomType = roomViewModel.RoomType;
        room.RoomPrice = roomViewModel.RoomPrice;
        room.Availabilty = roomViewModel.Availabilty;

        var success = await _roomService.UpdateEntity(room);

        if (!success)
        {
            return View();
        }

        return RedirectToAction(nameof(Index));
    }
}
