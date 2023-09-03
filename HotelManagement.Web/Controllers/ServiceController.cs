using HotelManagement.Web.Models.Dtos;
using HotelManagement.Web.Models.ViewModels;
using HotelManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagement.Web.Controllers;

public class ServiceController : Controller
{
    private readonly IGenericApiService<Service> _serviceService;
    private readonly IGenericApiService<Hotel> _hotelService;

    public ServiceController(
        IGenericApiService<Service> serviceService,
        IGenericApiService<Hotel> hotelService)
    {
        _serviceService = serviceService;
        _hotelService = hotelService;
    }

    public async Task<IActionResult> Index()
    {
        var services = await _serviceService.FetchEntities();

        var serviceViewModels = new List<ServiceViewModel>();

        foreach (var service in services)
        {
            var hotel = await _hotelService.FetchEntity(service.HotelId);

            var serviceVM = new ServiceViewModel
            {
                ServiceId = service.Id,
                Name = service.Name,
                Price = service.Price,
                HotelName = hotel.Name
            };

            serviceViewModels.Add(serviceVM);
        }

        return View(serviceViewModels);
    }

    public async Task<IActionResult> AddService()
    {
        var hotels = await _hotelService.FetchEntities();

        var serviceVM = new ServiceViewModel
        {
            Hotels = new SelectList(hotels, "Id", "Name")
        };

        return View(serviceVM);
    }

    [HttpPost]
    public async Task<IActionResult> AddService(ServiceViewModel serviceVM)
    {
        var service = new Service
        {
            Name = serviceVM.Name,
            Price = serviceVM.Price,
            HotelId = serviceVM.HotelId
        };

        var addedService = await _serviceService.AddEntity(service);

        if (addedService is null)
        {
            return View();
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ManageService(int serviceId)
    {
        var service = await _serviceService.FetchEntity(serviceId);

        if (service is null)
        {
            return NotFound($"Service with id = {serviceId} cannot be found");
        }

        var hotels = await _hotelService.FetchEntities();

        var serviceVM = new ServiceViewModel
        {
            Hotels = new SelectList(hotels, "Id", "Name"),
            HotelId = service.HotelId,
            Price = service.Price,
            Name = service.Name
        };

        return View(serviceVM);
    }

    [HttpPost]
    public async Task<IActionResult> ManageService(ServiceViewModel serviceVM, int serviceId)
    {
        var service = await _serviceService.FetchEntity(serviceId);

        if (service is null)
        {
            return View();
        }

        service.Name = serviceVM.Name;
        service.Price = serviceVM.Price;
        service.HotelId = serviceVM.HotelId;

        var success = await _serviceService.UpdateEntity(service);

        if (!success)
        {
            return View();
        }

        return RedirectToAction(nameof(Index));
    }
}
