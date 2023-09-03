using HotelManagement.Web.Models.Dtos;
using HotelManagement.Web.Models.ViewModels;
using HotelManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Text.RegularExpressions;

namespace HotelManagement.Web.Controllers;
public class EmployeeController : Controller
{
    private readonly IGenericApiService<Employee> _employeeService;
    private readonly IGenericApiService<Hotel> _hotelService;

    public EmployeeController(
        IGenericApiService<Employee> employeeService,
        IGenericApiService<Hotel> hotelService)
    {
        _employeeService = employeeService;
        _hotelService = hotelService;
    }

    public async Task<IActionResult> Index()
    {
        var employees = await _employeeService.FetchEntities();

        var employeeViewModels = new List<EmployeeViewModel>();

        foreach (var employee in employees)
        {
            var hotel = await _hotelService.FetchEntity(employee.HotelId);

            var employeeVM = new EmployeeViewModel
            {
                EmployeeId = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Position = employee.Position,
                Email = employee.Email,
                Phone = employee.Phone,
                HotelName = hotel.Name,
            };

            employeeViewModels.Add(employeeVM);
        }

        return View(employeeViewModels);
    }

    public async Task<IActionResult> AddEmployee()
    {
        var hotels = await _hotelService.FetchEntities();

        var employeeVM = new EmployeeViewModel
        {
            Hotels = new SelectList(hotels, "Id", "Name")
        };

        return View(employeeVM);
    }

    [HttpPost]
    public async Task<IActionResult> AddEmployee(EmployeeViewModel employeeViewModel)
    {
        var employee = new Employee
        {
            FirstName = employeeViewModel.FirstName,
            LastName = employeeViewModel.LastName,
            Position = employeeViewModel.Position,
            Email = employeeViewModel.Email,
            Phone = employeeViewModel.Phone,
            HotelId = employeeViewModel.HotelId
        };

        var addedEmployee = await _employeeService.AddEntity(employee);

        if (addedEmployee is null)
        {
            return View();
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ManageEmployee(int employeeId)
    {
        var employee = await _employeeService.FetchEntity(employeeId);

        if (employee is null)
        {
            return NotFound($"Employee with id = {employeeId} not found");
        }

        var hotels = await _hotelService.FetchEntities();

        var employeeViewModel = new EmployeeViewModel
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Position = employee.Position,
            Email = employee.Email,
            Phone = employee.Phone,
            HotelId = employee.HotelId,
            Hotels = new SelectList(hotels, "Id", "Name")
        };

        return View(employeeViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> ManageEmployee(EmployeeViewModel employeeViewModel, int employeeId)
    {
        var employee = await _employeeService.FetchEntity(employeeId);

        if (employee is null)
        {
            return View();
        }

        employee.FirstName = employeeViewModel.FirstName;
        employee.LastName = employeeViewModel.LastName;
        employee.Position = employeeViewModel.Position;
        employee.Email = employeeViewModel.Email;
        employee.Phone = employeeViewModel.Phone;
        employee.HotelId = employeeViewModel.HotelId;

        var success = await _employeeService.UpdateEntity(employee);

        if (!success)
        {
            return View();
        }

        return RedirectToAction(nameof(Index));
    }
}
