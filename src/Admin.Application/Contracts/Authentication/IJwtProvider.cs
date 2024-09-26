using Admin.Domain.Entities.Admin;

namespace Admin.Application.Contracts.Authentication;

public interface IJwtProvider
{
    string Generate(User user);
}