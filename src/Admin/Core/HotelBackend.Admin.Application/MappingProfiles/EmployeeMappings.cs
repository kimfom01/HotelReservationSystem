using AutoMapper;
using HotelBackend.Admin.Application.Dtos.Employees;
using HotelBackend.Admin.Domain.Entities;

namespace HotelBackend.Admin.Application.MappingProfiles;

public class EmployeeMappings : Profile
{
    public EmployeeMappings()
    {
        CreateMap<Employee, GetEmployeeDto>()
            .ReverseMap();
        CreateMap<Employee, CreateEmployeeDto>()
            .ReverseMap();
    }
}