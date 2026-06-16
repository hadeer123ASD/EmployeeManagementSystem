using Company.Models;

namespace Company.Repositories;

public interface IDepartmentRepository
{
    Task<List<Department>> GetAll();
    Task Add(Department dept);
}
