using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IRepository<Employee> _repository;

    public EmployeeController(IRepository<Employee> repository)
    {
        _repository = repository;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetEmployee(int id)
    {
        var employee = await _repository.GetEntity(emp => emp.Id == id);

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
        var employees = await _repository.GetEntities(emp => true);

        return Ok(employees);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        await _repository.Delete(id);
        int deltedCount = await _repository.SaveChanges();

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
        await _repository.Update(employee);
        await _repository.SaveChanges();

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostEmployee(Employee employee)
    {
        var added = await _repository.Add(employee);
        await _repository.SaveChanges();

        return CreatedAtAction(nameof(GetEmployee), new { id = added.Id }, added);
    }
}
