using HotelManagement.Web.Models.Dtos;
using HotelManagement.Web.Models.ViewModels;
using HotelManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Web.Controllers;

public class RoomStatusController : Controller
{
    private readonly IGenericApiService<RoomStatus> _roomStatusService;
    private readonly IGenericApiService<Room> _roomService;
    private readonly IGenericApiService<Guest> _guestService;

    public RoomStatusController(
        IGenericApiService<RoomStatus> roomStatusService,
        IGenericApiService<Room> roomService,
        IGenericApiService<Guest> guestService)
    {
        _roomStatusService = roomStatusService;
        _roomService = roomService;
        _guestService = guestService;
    }

    public async Task<IActionResult> Index()
    {
        var roomStatuses = await _roomStatusService.FetchEntities();

        var roomStatusViewModels = new List<RoomStatusViewModel>();

        foreach (var status in roomStatuses)
        {
            var room = await _roomService.FetchEntity(status.RoomId);
            var guest = await _guestService.FetchEntity(status.GuestId);

            var roomStatusVM = new RoomStatusViewModel
            {
                RoomStatusId = status.Id,
                RoomType = room.RoomType,
                GuestName = guest.FirstName,
                CheckIn = status.CheckIn,
                CheckOut = status.CheckOut
            };

            roomStatusViewModels.Add(roomStatusVM);
        }

        return View(roomStatusViewModels);
    }
}
