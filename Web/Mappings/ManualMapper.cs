using Web.Data;
using Web.Models;
using Web.Models.Dtos;

namespace Web.Mappings;

public class ManualMapper
{
    public Guest MapUserToGuest(ApplicationUser user)
    {
        var guest = new Guest
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            MiddleName = user.MiddleName,
            Email = user.Email,
            Phone = user.PhoneNumber,
            DateOfBirth = user.DateOfBirth
        };

        return guest;
    }

    public async Task<UserViewModel> MapToUserViewModel(ApplicationUser? user,
        Func<ApplicationUser, Task<List<string>>> GetUserRoles)
    {
        return new UserViewModel
        {
            UserId = user.Id,
            UserName = user.UserName!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            Roles = await GetUserRoles(user)
        };
    }
}
