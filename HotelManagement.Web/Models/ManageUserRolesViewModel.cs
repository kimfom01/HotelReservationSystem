namespace HotelManagement.Web.Models;

public class ManageUserRolesViewModel
{
    public required string RoleId { get; set; }
    public required string RoleName { get; set; }
    public bool Selected { get; set; }
}