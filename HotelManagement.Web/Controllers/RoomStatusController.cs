using HotelManagement.Web.Models.Dtos;
using HotelManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Web.Controllers;

public class RoomStatusController : Controller
{
    private readonly IGenericApiService<RoomStatus> _roomStatusService;

    public RoomStatusController(IGenericApiService<RoomStatus> roomStatusService)
    {
        _roomStatusService = roomStatusService;
    }

    public IActionResult Index()
    {
        return View();
    }
}
