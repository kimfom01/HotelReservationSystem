using AutoMapper;
using HotelBackend.Admin.Application.Dtos.Employees;
using HotelBackend.Admin.Domain.Entities;

namespace HotelBackend.Admin.Application.MappingProfiles;

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