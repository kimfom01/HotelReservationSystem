using AutoMapper;
using HotelBackend.Old.Remove.DataAccess.Models;
using HotelBackend.Old.Remove.Api.Dtos;
using HotelBackend.Old.Remove.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Old.Remove.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IEmployeeService _employeeService;

    public EmployeeController(
        IEmployeeService employeeService,
        IMapper mapper)
    {
        _employeeService = employeeService;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetEmployee(int id)
    {
        var employee = await _employeeService.GetEmployee(id);

        if (employee is null)
        {
            return NotFound();
        }

        var employeeDto = _mapper.Map<EmployeeDto>(employee);

        return Ok(employeeDto);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetEmployees()
    {
        var employees = await _employeeService.GetEmployees();

        var employeesDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

        return Ok(employeesDtos);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        int deltedCount = await _employeeService.DeleteEmployee(id);

        if (deltedCount <= 0)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateEmployee(EmployeeDto employeeDto)
    {
        var employee = _mapper.Map<Employee>(employeeDto);

        await _employeeService.UpdateEmployee(employee);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostEmployee(EmployeeDto employeeDto)
    {
        var employee = _mapper.Map<Employee>(employeeDto);

        var added = await _employeeService.PostEmployee(employee);

        var employeeDtoResult = _mapper.Map<EmployeeDto>(employee);

        return CreatedAtAction(nameof(GetEmployee), new { id = employeeDtoResult.Id }, employeeDtoResult);
    }
}
