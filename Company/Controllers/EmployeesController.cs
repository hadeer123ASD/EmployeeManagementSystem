using Company.DTOs;
using Company.Models;
using Company.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly EmployeeService _service;

    public EmployeesController(EmployeeService service)
    {
        _service = service;
    }

    // GET ALL
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _service.GetAll();

        var result = employees.Select(e => new EmployeeDTO
        {
            EmployeeId = e.EmployeeId,
            FullName = e.FullName,
            Email = e.Email,
            MobileNumber = e.MobileNumber,
            DepartmentId = e.DepartmentId,
            DepartmentName = e.Department != null ? e.Department.DepartmentName : null,
            JobTitle = e.JobTitle,
            HireDate = e.HireDate,
            IsActive = e.IsActive
        });

        return Ok(result);
    }

    // GET BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var e = await _service.GetById(id);
        if (e == null) return NotFound();

        var result = new EmployeeDTO
        {
            EmployeeId = e.EmployeeId,
            FullName = e.FullName,
            Email = e.Email,
            MobileNumber = e.MobileNumber,
            DepartmentId = e.DepartmentId,
            DepartmentName = e.Department?.DepartmentName,
            JobTitle = e.JobTitle,
            HireDate = e.HireDate,
            IsActive = e.IsActive
        };

        return Ok(result);
    }

    // ADD
    [HttpPost]
    public async Task<IActionResult> Add(EmployeeDTO dto)
    {
        var employee = new Employee
        {
            FullName = dto.FullName,
            Email = dto.Email,
            MobileNumber = dto.MobileNumber,
            DepartmentId = dto.DepartmentId,
            JobTitle = dto.JobTitle,
            HireDate = dto.HireDate,
            IsActive = dto.IsActive
        };

        await _service.Add(employee);
        return Ok("Employee Added");
    }

    // UPDATE (FIXED)
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, EmployeeDTO dto)
    {
        var employee = new Employee
        {
            EmployeeId = id,
            FullName = dto.FullName,
            Email = dto.Email,
            MobileNumber = dto.MobileNumber,
            DepartmentId = dto.DepartmentId,
            JobTitle = dto.JobTitle,
            HireDate = dto.HireDate,
            IsActive = dto.IsActive
        };

        await _service.Update(employee);
        return Ok("Employee Updated");
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return Ok("Employee Deleted");
    }

    // SEARCH
    [HttpGet("search")]
    public async Task<IActionResult> Search(string? name, int? deptId)
    {
        var employees = await _service.Search(name, deptId);

        var result = employees.Select(e => new EmployeeDTO
        {
            EmployeeId = e.EmployeeId,
            FullName = e.FullName,
            Email = e.Email,
            MobileNumber = e.MobileNumber,
            DepartmentId = e.DepartmentId,
            DepartmentName = e.Department?.DepartmentName,
            JobTitle = e.JobTitle,
            HireDate = e.HireDate,
            IsActive = e.IsActive
        });

        return Ok(result);
    }
}