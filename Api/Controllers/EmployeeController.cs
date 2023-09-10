using Api.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IDataServiceGeneric<Employee> _dataService;

    public EmployeeController(IDataServiceGeneric<Employee> dataService)
    {
        _dataService = dataService;
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

        return Ok(employee);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetEmployees()
    {
        var employees = await _dataService.GetEntities();

        return Ok(employees);
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
    public async Task<IActionResult> UpdateEmployee(Employee employee)
    {
        await _dataService.UpdateEntity(employee);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostEmployee(Employee employee)
    {
        var added = await _dataService.PostEntity(employee);

        return CreatedAtAction(nameof(GetEmployee), new { id = added.Id }, added);
    }
}
