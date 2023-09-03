using HotelManagement.Web.Models.Dtos;
using HotelManagement.Web.Models.ViewModels;
using HotelManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

    //public async Task<IActionResult> AddRoomStatus()
    //{
    //    var rooms = await _roomService.FetchEntities();
    //    var guests = await _guestService.FetchEntities();

    //    var roomServiceVM = new RoomStatusViewModel
    //    {
    //        Rooms = new SelectList(rooms, "Id", "RoomType"),
    //        Guests = new SelectList(guests, "Id", "FirstName")
    //    };

    //    return View(roomServiceVM);
    //}

    //[HttpPost]
    //public async Task<IActionResult> AddRoomStatus(RoomStatusViewModel roomStatusViewModel)
    //{
    //    var guest = await _guestService.FetchEntity(roomStatusViewModel.GuestId);

    //    var roomStatus = new RoomStatus
    //    {
    //        RoomId = roomStatusViewModel.RoomId,
    //        GuestId = roomStatusViewModel.GuestId,
    //        ReservationId = roomStatusViewModel.ReservationId,
    //        CheckIn = roomStatusViewModel.CheckIn,
    //        CheckOut = roomStatusViewModel.CheckOut
    //    };

    //    var addedStatus = await _roomStatusService.AddEntity(roomStatus);

    //    if (addedStatus is null)
    //    {
    //        return View();
    //    }

    //    return RedirectToAction(nameof(Index));
    //}

    public async Task<IActionResult> ManageRoomStatus(int statusId)
    {
        var roomStatus = await _roomStatusService.FetchEntity(statusId);

        if (roomStatus is null)
        {
            return View();
        }

        var rooms = await _roomService.FetchEntities();

        var roomStatusVM = new RoomStatusViewModel
        {
            CheckIn = roomStatus.CheckIn,
            CheckOut = roomStatus.CheckOut,
            Rooms = new SelectList(rooms, "Id", "RoomType"),
            RoomId = roomStatus.RoomId
        };

        return View(roomStatusVM);
    }

    [HttpPost]
    public async Task<IActionResult> ManageRoomStatus(RoomStatusViewModel roomStatusViewModel, int statusId)
    {
        var roomStatus = await _roomStatusService.FetchEntity(statusId);

        if (roomStatus is null)
        {
            return NotFound($"Room Status with id = {statusId} not found");
        }

        roomStatus.RoomId = roomStatusViewModel.RoomId;
        roomStatus.CheckIn = roomStatusViewModel.CheckIn;
        roomStatus.CheckOut = roomStatusViewModel.CheckOut;

        var success = await _roomStatusService.UpdateEntity(roomStatus);

        if (!success)
        {
            return View();
        }

        return RedirectToAction(nameof(Index));
    }
}
