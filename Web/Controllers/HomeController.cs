using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Models;
using Web.Models.Dtos;
using Web.Services;

namespace HotelManagement.Web.Controllers;

public class HomeController : Controller
{
    private readonly IGenericApiService<Guest> _guestService;

    public HomeController(IGenericApiService<Guest> guestService)
    {
        _guestService = guestService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
