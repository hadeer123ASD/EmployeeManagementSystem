using Company.Models;
using Company.Repositories;

namespace Company.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _repo;

    public EmployeeService(IEmployeeRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Employee>> GetAll() => _repo.GetAll();
    public Task<Employee?> GetById(int id) => _repo.GetById(id);
    public Task Add(Employee emp) => _repo.Add(emp);
    public Task Update(Employee emp) => _repo.Update(emp);
    public Task Delete(int id) => _repo.Delete(id);
    public Task<List<Employee>> Search(string? name, int? deptId) => _repo.Search(name, deptId);
}