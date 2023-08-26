using HotelManagement.Web.Data;
using HotelManagement.Web.Mappings;
using HotelManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Web.Controllers;

[Authorize(Roles = nameof(Roles.SystemAdmin))]
public class UserManagementController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ManualMapper _mapper;

    public UserManagementController(
        UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole> roleManager,
        ManualMapper mapper
        )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.ToListAsync();
        var userRolesViewModel = new List<UserViewModel>();

        foreach (var user in users)
        {
            UserViewModel currentUserRolesViewModel = await _mapper.MapToUserViewModel(user, GetUserRoles);

            userRolesViewModel.Add(currentUserRolesViewModel);
        }

        return View(userRolesViewModel);
    }

    private async Task<List<string>> GetUserRoles(ApplicationUser user)
    {
        return new List<string>(await _userManager.GetRolesAsync(user));
    }

    public async Task<IActionResult> ManageUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            ViewBag.ErrorMessage = $"User with id = {userId} cannot be found";
            return NotFound($"User with id = {userId} cannot be found");
        }

        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> ManageUser(ApplicationUser manageUserUpdateInfo, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            ViewBag.ErrorMessage = $"User with id = {userId} cannot be found";
            return NotFound($"User with id = {userId} cannot be found");
        }

        user.FirstName = manageUserUpdateInfo.FirstName;
        user.LastName = manageUserUpdateInfo.LastName;
        user.MiddleName = manageUserUpdateInfo.MiddleName;
        user.Email = manageUserUpdateInfo.Email;
        user.DateOfBirth = manageUserUpdateInfo.DateOfBirth;
        user.PhoneNumber = manageUserUpdateInfo.PhoneNumber;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            return RedirectToAction(nameof(Index));
        }

        return View();
    }

    public async Task<IActionResult> ManageRoles(string userId)
    {
        ViewBag.userId = userId;

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            ViewBag.ErrorMessage = $"User with id = {userId} cannot be found";
            return NotFound($"User with id = {userId} cannot be found");
        }

        ViewBag.username = user.UserName!;

        var model = new List<ManageUserRolesViewModel>();
        foreach (var role in _roleManager.Roles.ToList())
        {
            var userRolesViewModel = new ManageUserRolesViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name!
            };

            if (await _userManager.IsInRoleAsync(user, role.Name!))
            {
                userRolesViewModel.Selected = true;
            }
            else
            {
                userRolesViewModel.Selected = false;
            }

            model.Add(userRolesViewModel);
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ManageRoles(List<ManageUserRolesViewModel> model, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return View();
        }

        var roles = await _userManager.GetRolesAsync(user);
        var result = await _userManager.RemoveFromRolesAsync(user, roles);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Cannot remove user existing roles");
            return View(model);
        }

        result = await _userManager.AddToRolesAsync(user, model.Where(role => role.Selected).Select(role => role.RoleName));
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Cannot add selected roles to user");
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }
}
