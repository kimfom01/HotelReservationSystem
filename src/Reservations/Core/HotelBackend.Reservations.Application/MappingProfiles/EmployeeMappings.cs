using AutoMapper;
using HotelBackend.Reservations.Application.Dtos.Employees;
using HotelBackend.Reservations.Domain.Entities.Admin;

namespace HotelBackend.Reservations.Application.MappingProfiles;

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