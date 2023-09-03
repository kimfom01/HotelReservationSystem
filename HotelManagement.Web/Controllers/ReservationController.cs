using HotelManagement.Web.Data;
using HotelManagement.Web.Models.Dtos;
using HotelManagement.Web.Models.ViewModels;
using HotelManagement.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagement.Web.Controllers;

[Authorize]
public class ReservationController : Controller
{
    private readonly IGenericApiService<Reservation> _reservationService;
    private readonly IGenericApiService<Hotel> _hotelService;
    private readonly IRoomApiService _roomService;
    private readonly IGuestApiService _guestService;
    private readonly UserManager<ApplicationUser> _userManager;

    public ReservationController(
        IGenericApiService<Reservation> reservationService,
        IGenericApiService<Hotel> hotelService,
        IRoomApiService roomService,
        IGuestApiService guestService,
        UserManager<ApplicationUser> userManager)
    {
        _reservationService = reservationService;
        _hotelService = hotelService;
        _roomService = roomService;
        _guestService = guestService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var hotels = await _hotelService.FetchEntities();

        int[] capacities = { 3, 4, 5 };

        var reservationViewModel = new ReservationViewModel
        {
            Hotels = new SelectList(hotels, "Id", "Name"),
            Capacities = new SelectList(capacities)
        };

        return View(reservationViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Index(ReservationViewModel reservationViewModel)
    {
        var room = await _roomService.FetchRoomByHotelId(reservationViewModel.HotelId, reservationViewModel.NumberOfGuests);

        if (room is null)
        {
            return View();
        }

        var tax = 100M; //Don't know where these data come from lol
        var discount = 0.0;

        int guestId = await GetGuestId();

        var reservation = new Reservation
        {
            Date = reservationViewModel.Date,
            CheckIn = reservationViewModel.CheckIn,
            CheckOut = reservationViewModel.CheckOut,
            GuestId = guestId,
            HotelId = reservationViewModel.HotelId,
            NumberOfGuests = reservationViewModel.NumberOfGuests,
            Price = room.RoomPrice,
            Tax = tax,
            Discount = discount,
        };

        var reserved = await _reservationService.AddEntity(reservation);

        if (reserved is null)
        {
            return View();
        }

        // set room status

        return RedirectToAction(nameof(ViewReservationList));
    }

    private async Task<int> GetGuestId()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user is null)
        {
            return default;
        }

        var emailAddress = user.Email;

        var guest = await _guestService.FetchGuestByEmailAddress(emailAddress!);

        var guestId = guest!.Id;

        return guestId;
    }

    public async Task<IActionResult> ViewReservationList()
    {
        var reservations = await _reservationService.FetchEntities();

        return View(reservations);
    }

    public async Task<IActionResult> ManageReservation(int reservationId)
    {
        var hotels = await _hotelService.FetchEntities();

        var reservation = await _reservationService.FetchEntity(reservationId);

        if (reservation is null)
        {
            return NotFound($"Reservation with id = {reservationId} cannot be found");
        }

        var reservationViewModel = new ReservationViewModel
        {
            Date = reservation.Date,
            CheckIn = reservation.CheckIn,
            CheckOut = reservation.CheckOut,
            HotelId = reservation.HotelId,
            NumberOfGuests = reservation.NumberOfGuests,
            Hotels = new SelectList(hotels, "Id", "Name")
        };

        return View(reservationViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> ManageReservation(ReservationViewModel reservationViewModel, int reservationId)
    {

        var reservation = await _reservationService.FetchEntity(reservationId);

        if (reservation is null)
        {
            return NotFound($"Reservation with id = {reservationId} cannot be found");
        }

        var room = await _roomService.FetchRoomByHotelId(reservationViewModel.HotelId, reservationViewModel.NumberOfGuests);

        reservation.CheckIn = reservationViewModel.CheckIn;
        reservation.CheckOut = reservationViewModel.CheckOut;
        reservation.HotelId = reservationViewModel.HotelId;
        reservation.NumberOfGuests = reservationViewModel.NumberOfGuests;
        reservation.Price = room.RoomPrice;

        var success = await _reservationService.UpdateEntity(reservation);

        if (!success)
        {
            return View();
        }

        return RedirectToAction(nameof(ViewReservationList));
    }
}
