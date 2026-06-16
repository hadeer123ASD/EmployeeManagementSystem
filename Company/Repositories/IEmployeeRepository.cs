using Company.Models;

namespace Company.Repositories;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAll();
    Task<Employee?> GetById(int id);
    Task Add(Employee employee);
    Task Update(Employee employee);
    Task Delete(int id);
    Task<List<Employee>> Search(string? name, int? deptId);
}
