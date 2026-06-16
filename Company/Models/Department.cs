namespace Company.Models;

public class Department
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;

    public List<Employee>? Employees { get; set; }
}
