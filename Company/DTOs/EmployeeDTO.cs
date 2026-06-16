namespace Company.DTOs;

public class EmployeeDTO
{
    public int? EmployeeId { get; set; } // مهم للـ update
    public string FullName { get; set; }
    public string Email { get; set; }
    public string MobileNumber { get; set; }
    public int DepartmentId { get; set; }
    public string JobTitle { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsActive { get; set; }

    public string? DepartmentName { get; set; }
}