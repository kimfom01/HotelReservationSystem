namespace HotelBackend.Common.Options;

public class JwtConfigOptions
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
}