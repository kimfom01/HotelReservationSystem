namespace Hrs.AdminCli.Models;

internal record Admin(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string ConfirmPassword);