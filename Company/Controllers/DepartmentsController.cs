using Company.DTOs;
using Company.Models;
using Company.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly DepartmentService _service;

    public DepartmentsController(DepartmentService service)
    {
        _service = service;
    }

    // GET ALL
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var departments = await _service.GetAll();

        var result = departments.Select(d => new DepartmentDto
        {
            DepartmentId = d.DepartmentId,
            DepartmentName = d.DepartmentName
        });

        return Ok(result);
    }

    // ADD
    [HttpPost]
    public async Task<IActionResult> Add(DepartmentDto dto)
    {
        var department = new Department
        {
            DepartmentName = dto.DepartmentName
        };

        await _service.Add(department);
        return Ok("Department Added");
    }
}