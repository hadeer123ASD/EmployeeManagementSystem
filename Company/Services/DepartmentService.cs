using Company.Models;
using Company.Repositories;

namespace Company.Services;

public class DepartmentService
{
    private readonly IDepartmentRepository _repo;

    public DepartmentService(IDepartmentRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Department>> GetAll() => _repo.GetAll();
    public Task Add(Department dept) => _repo.Add(dept);
}
