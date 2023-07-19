namespace HotelManagement.Models;

public class Service
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int HotelId { get; set; }
    public Hotel Hotel { get; set; }
}
