namespace Admin.Application.Dtos.Admin.Users;

public record LoginUserRequest
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}