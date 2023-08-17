using HotelManagement.Web.Models;
using HotelManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HotelManagement.Web.Controllers;

public class HomeController : Controller
{
    private readonly IGuestService _guestService;

    public HomeController(IGuestService guestService)
    {
        _guestService = guestService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Privacy()
    {
        var guestList = await _guestService.FetchGuests();

        return View(guestList);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
