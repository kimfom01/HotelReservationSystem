using Hrs.Domain.Entities.Admin;

namespace Hrs.Application.Contracts.Authentication;

public interface IJwtProvider
{
    string Generate(Employee employee);
}