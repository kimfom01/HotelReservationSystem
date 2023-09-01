using HotelManagement.Web.Models.Dtos;
using HotelManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Web.Controllers;
public class GuestController : Controller
{
    private readonly IGenericApiService<Guest> _guestService;

    public GuestController(IGenericApiService<Guest> guestService)
    {
        _guestService = guestService;
    }

    public async Task<IActionResult> Index()
    {
        var guests = await _guestService.FetchEntities();

        return View(guests);
    }
}
