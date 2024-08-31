namespace Hrs.Application.Dtos.Admin.Users;

public record LoginUserResponse
{
    public string Token { get; init; } = string.Empty;

    public string TokenType { get; init; } = string.Empty;
}