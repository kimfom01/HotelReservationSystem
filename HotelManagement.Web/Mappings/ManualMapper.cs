using HotelManagement.Web.Data;
using HotelManagement.Web.Models.Dtos;

namespace HotelManagement.Web.Mappings;

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
}
