using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.ViewModels;

public class MealViewModel
{
    public int MealId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int MealPrice { get; set; }
    public int HotelId { get; set; }
    public string HotelName { get; set; }
    public SelectList? Hotels { get; set; }
}
