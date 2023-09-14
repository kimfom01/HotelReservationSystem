namespace Web.Models;

public class BaseResponse
{
    public int StatusCode { get; set; }
    public string? ErrorMessage { get; set; }
}
