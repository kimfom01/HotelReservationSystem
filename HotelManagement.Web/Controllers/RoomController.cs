using HotelManagement.Web.Models.Dtos;
using HotelManagement.Web.Models.ViewModels;
using HotelManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
}
