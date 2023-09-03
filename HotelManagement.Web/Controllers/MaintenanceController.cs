using HotelManagement.Web.Models.Dtos;
using HotelManagement.Web.Models.ViewModels;
using HotelManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HotelManagement.Web.Controllers;

public class MaintenanceController : Controller
{
    private readonly IGenericApiService<Maintenance> _maintenanceService;
    private readonly IGenericApiService<Room> _roomService;

    public MaintenanceController(
        IGenericApiService<Maintenance> maintenanceService,
        IGenericApiService<Room> roomService)
    {
        _maintenanceService = maintenanceService;
        _roomService = roomService;
    }

    public async Task<IActionResult> Index()
    {
        var maintenances = await _maintenanceService.FetchEntities();

        var maintenanceViewModels = new List<MaintenanceViewModel>();

        foreach (var maintenance in maintenances)
        {
            var room = await _roomService.FetchEntity(maintenance.RoomId);

            var maintenanceVM = new MaintenanceViewModel
            {
                MaintenanceId = maintenance.Id,
                RoomNumber = room.RoomNumber,
                MaintenanceType = maintenance.MaintenanceType,
                StartDate = maintenance.StartDate,
                EndDate = maintenance.EndDate
            };

            maintenanceViewModels.Add(maintenanceVM);
        }

        return View(maintenanceViewModels);
    }

    public async Task<IActionResult> AddMaintenance()
    {
        var rooms = await _roomService.FetchEntities();

        var maintenanceVM = new MaintenanceViewModel
        {
            Rooms = new SelectList(rooms, "Id", "RoomNumber")
        };

        return View(maintenanceVM);
    }

    [HttpPost]
    public async Task<IActionResult> AddMaintenance(MaintenanceViewModel maintenanceViewModel)
    {
        var maintenance = new Maintenance
        {
            RoomId = maintenanceViewModel.RoomId,
            MaintenanceType = maintenanceViewModel.MaintenanceType,
            StartDate = maintenanceViewModel.StartDate,
            EndDate = maintenanceViewModel.EndDate
        };

        var added = await _maintenanceService.AddEntity(maintenance);

        if (added is null)
        {
            return View();
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ManageMaintenance(int maintenanceId)
    {
        var maintenance = await _maintenanceService.FetchEntity(maintenanceId);

        if (maintenance is null)
        {
            return NotFound($"Maintenance with id = {maintenanceId} not found");
        }

        var rooms = await _roomService.FetchEntities();

        var maintenanceViewModel = new MaintenanceViewModel
        {
            Rooms = new SelectList(rooms, "Id", "RoomNumber"),
            MaintenanceType = maintenance.MaintenanceType,
            StartDate = maintenance.StartDate,
            EndDate = maintenance.EndDate,
            RoomId = maintenance.RoomId
        };

        return View(maintenanceViewModel);
    }
}
