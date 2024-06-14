namespace Hrs.Application.Contracts.Authentication;

public interface IPasswordManager
{
    bool VerifyPassword(string password, string hash);
    string HashPassword(string password);
}