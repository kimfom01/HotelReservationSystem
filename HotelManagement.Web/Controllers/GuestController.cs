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

    public IActionResult AddGuest()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddGuest(Guest guest)
    {
        var addedGuest = await _guestService.AddEntity(guest);

        if (addedGuest is null)
        {
            return View();
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ManageGuest(int guestId)
    {
        var guest = await _guestService.FetchEntity(guestId);

        if (guest is null)
        {
            return NotFound($"Guest with id = {guestId} cannot be found");
        }

        return View(guest);
    }


    [HttpPost]
    public async Task<IActionResult> ManageGuest(Guest newGuestInfo, int guestId)
    {
        var guest = await _guestService.FetchEntity(guestId);

        if (guest is null)
        {
            return View();
        }

        guest.FirstName = newGuestInfo.FirstName;
        guest.LastName = newGuestInfo.LastName;
        guest.MiddleName = newGuestInfo.MiddleName;
        guest.Email = newGuestInfo.Email;
        guest.Phone = newGuestInfo.Phone;
        guest.DateOfBirth = newGuestInfo.DateOfBirth;

        var success = await _guestService.UpdateEntity(guest);

        if (!success)
        {
            return View();
        }

        return RedirectToAction(nameof(Index));
    }
}
