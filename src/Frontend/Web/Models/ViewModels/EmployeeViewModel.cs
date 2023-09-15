using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.ViewModels;

public class EmployeeViewModel
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int HotelId { get; set; }
    public string HotelName { get; set; }
    public SelectList? Hotels { get; set; }
}
