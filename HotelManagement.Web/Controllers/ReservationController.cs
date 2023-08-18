using HotelManagement.Web.Models.Dtos;
using HotelManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Web.Controllers;

public class ReservationController : Controller
{
    private readonly IGenericApiService<Reservation> _reservationService;

    public ReservationController(IGenericApiService<Reservation> reservationService)
    {
        _reservationService = reservationService;
    }

    public async Task<IActionResult> Index()
    {
        var reservations = await _reservationService.FetchEntities();

        return View(reservations);
    }
}
