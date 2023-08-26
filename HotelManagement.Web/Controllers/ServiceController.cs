using HotelManagement.Web.Models.Dtos;
using HotelManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Web.Controllers;
public class ServiceController : Controller
{
    private readonly IGenericApiService<Service> _serviceService;

    public ServiceController(IGenericApiService<Service> serviceService)
    {
        _serviceService = serviceService;
    }

    public async Task<IActionResult> Index()
    {
        var services = await _serviceService.FetchEntities();

        return View(services);
    }

    public IActionResult AddService()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddService(Service service)
    {
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

        return View(service);
    }

    [HttpPost]
    public async Task<IActionResult> ManageService(Service newServiceInfo, int serviceId)
    {
        var service = await _serviceService.FetchEntity(serviceId);

        if (service is null)
        {
            return View();
        }

        service.Name = newServiceInfo.Name;
        service.Price = newServiceInfo.Price;
        service.HotelId = newServiceInfo.HotelId;

        var success = await _serviceService.UpdateEntity(service);

        if (!success)
        {
            return View();
        }

        return RedirectToAction(nameof(Index));
    }
}
