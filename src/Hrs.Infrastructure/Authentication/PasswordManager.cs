using Hrs.Application.Contracts.Authentication;

namespace Hrs.Infrastructure.Authentication;

public class PasswordManager : IPasswordManager
{
    public bool VerifyPassword(string password, string hash)
    {
        var verified = BCrypt.Net.BCrypt.Verify(password, hash);

        return verified;
    }

    public string HashPassword(string password)
    {
        var hash = BCrypt.Net.BCrypt.HashPassword(password);

        return hash;
    }
}