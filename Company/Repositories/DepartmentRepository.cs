using Company.Data;
using Company.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly AppDbContext _context;

    public DepartmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Department>> GetAll()
        => await _context.Departments.ToListAsync();

    //public async Task Add(Department dept)
    //{
    //    await _context.Departments.AddAsync(dept);
    //    await _context.SaveChangesAsync();
    //}

    public async Task Add(Department dept)
    {
        try
        {
            await _context.Departments.AddAsync(dept);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message);
            throw;
        }
    }
}