namespace HotelBackend.Old.Remove.DataAccess.Models;

public class HotelAmenity
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public Hotel? Hotel { get; set; }
    public string Name { get; set; }
}
