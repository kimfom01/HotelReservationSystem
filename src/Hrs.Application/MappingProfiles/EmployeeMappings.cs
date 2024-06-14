using AutoMapper;
using Hrs.Domain.Entities.Admin;
using Hrs.Application.Dtos.Admin.Employees;

namespace Hrs.Application.MappingProfiles;

public class EmployeeMappings : Profile
{
    public EmployeeMappings()
    {
        CreateMap<Employee, GetEmployeeResponse>()
            .ReverseMap();
        CreateMap<Employee, CreateEmployeeRequest>()
            .ReverseMap();
    }
}