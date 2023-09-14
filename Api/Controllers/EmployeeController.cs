using Api.Dtos;
using Api.Services;
using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IDataServiceGeneric<Employee> _dataService;
    private readonly IMapper _mapper;

    public EmployeeController(IDataServiceGeneric<Employee> dataService,
        IMapper mapper)
    {
        _dataService = dataService;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetEmployee(int id)
    {
        var employee = await _dataService.GetEntity(id);

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
        var employees = await _dataService.GetEntities();

        var employeesDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

        return Ok(employeesDtos);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        int deltedCount = await _dataService.DeleteEntity(id);

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

        await _dataService.UpdateEntity(employee);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostEmployee(EmployeeDto employeeDto)
    {
        var employee = _mapper.Map<Employee>(employeeDto);

        var added = await _dataService.PostEntity(employee);

        var employeeDtoResult = _mapper.Map<EmployeeDto>(employee);

        return CreatedAtAction(nameof(GetEmployee), new { id = employeeDtoResult.Id }, employeeDtoResult);
    }
}
