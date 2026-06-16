using Company.Data;
using Company.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Employee>> GetAll()
        => await _context.Employees
            .Include(e => e.Department)
            .ToListAsync();

    public async Task<Employee?> GetById(int id)
        => await _context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.EmployeeId == id);

    public async Task Add(Employee employee)
    {
        var exists = await _context.Employees
            .AnyAsync(e => e.Email == employee.Email);

        if (exists)
            throw new Exception("Email already exists");

        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
    }


    // ✅ FIXED UPDATE (IMPORTANT)
    public async Task Update(Employee employee)
    {
        var existing = await _context.Employees
            .FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);

        if (existing == null)
            throw new Exception("Employee not found");

        existing.FullName = employee.FullName;
        existing.Email = employee.Email;
        existing.MobileNumber = employee.MobileNumber;
        existing.DepartmentId = employee.DepartmentId;
        existing.JobTitle = employee.JobTitle;
        existing.HireDate = employee.HireDate;
        existing.IsActive = employee.IsActive;

        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var emp = await _context.Employees.FindAsync(id);

        if (emp == null)
            return;

        _context.Employees.Remove(emp);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Employee>> Search(string? name, int? deptId)
    {
        var query = _context.Employees
            .Include(e => e.Department)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
        {
            name = name.Trim().ToLower();

            query = query.Where(e =>
                e.FullName.ToLower().Contains(name));
        }

        if (deptId.HasValue)
            query = query.Where(e => e.DepartmentId == deptId);

        return await query.ToListAsync();
    }
}



