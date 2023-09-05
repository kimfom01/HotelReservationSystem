namespace DataAccess.Models;

public class Meal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public decimal MealPrice { get; set; }
    public int HotelId { get; set; }
    public Hotel? Hotel { get; set; }
}
