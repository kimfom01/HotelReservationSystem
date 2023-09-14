namespace Api.Dtos;

public class Meal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int MealPrice { get; set; }
    public int HotelId { get; set; }
}
